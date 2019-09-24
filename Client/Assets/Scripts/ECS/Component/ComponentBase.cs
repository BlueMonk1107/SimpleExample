using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

public abstract class ComponentBase<T> : IComponent where T : ComponentBase<T>,new()
{
    private T _last;
    private Func<T> _createNewFunc;
    private Action<T, T> _cloneObjValue;
    private Func<T, T, bool> _valueChange;
    
    
    public bool ValueChanged()
    {
        //获取新的对象_last，作为副本，保存之前数据
        //比对两个对象的值
        //保存当前的结果 当前值是否改变
        //更新_last数据
        T self = this as T;
        if (_last == null)
        {
            //1.typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
            //2._last= new T();
            //3.Activator.CreateInstance(typeof(T)) Activator.CreateInstance<T>()
            _last = CreateNew();
            _cloneObjValue = CloneObjValue();
            _valueChange = ValueChange();
            _cloneObjValue(self, _last);
            return true;
        }

        bool result = _valueChange(self, _last);
        _cloneObjValue(self, _last);
        return result;
    }

    private T CreateNew()
    {
        if (_createNewFunc == null)
        {
            Type type = typeof(T);
            var conInfo = type.GetConstructor(Type.EmptyTypes);
        
            DynamicMethod dynamicMethod = new DynamicMethod(string.Format("Create{0}",Guid.NewGuid()),type,null);
            var generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Newobj,conInfo);
            generator.Emit(OpCodes.Ret);

            _createNewFunc = dynamicMethod.CreateDelegate(typeof(Func<T>)) as Func<T>;
        }

        return _createNewFunc();
    }

    private Action<T,T> CloneObjValue()
    {
        var self = Expression.Parameter(typeof(T));
        var last = Expression.Parameter(typeof(T));

        var body = typeof(T).GetProperties().Select(x =>
            Expression.Assign(
                Expression.MakeMemberAccess(last, x),
                Expression.MakeMemberAccess(self, x)
            ));
        var block = Expression.Block(body);

        return Expression.Lambda<Action<T, T>>(block, self, last).Compile();
    }

    private Func<T, T, bool> ValueChange()
    {
        var self = Expression.Parameter(typeof(T));
        var last = Expression.Parameter(typeof(T));
        LabelTarget labelTarget = Expression.Label(typeof(bool));
        
        var body = typeof(T).GetProperties().Select(x =>
            {
                var test = Expression.NotEqual(
                    Expression.MakeMemberAccess(last, x),
                    Expression.MakeMemberAccess(self, x));
                var ifTrue = Expression.Return(labelTarget, Expression.Constant(true));
                return Expression.IfThen(test, ifTrue);
            }
            );
        var block = Expression.Block(body.Concat(new Expression[]
        {
            Expression.Label(labelTarget,Expression.Constant(false))
        }));

        return Expression.Lambda<Func<T, T, bool>>(block, self, last).Compile();
    }
}
