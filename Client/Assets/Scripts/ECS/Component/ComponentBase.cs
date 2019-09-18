using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public abstract class ComponentBase<T> : IComponent where T : ComponentBase<T>
{
    private T _last;
    private Func<T, T, bool> _onValueChange;
    private Func<T, T> _onCloneObject;

    public bool ValueChanged()
    {
        T self = this as T;
        if (_last == null)
        {
            _onCloneObject = CloneObj<T>();
            _last = _onCloneObject(self);
            _onValueChange = ValueChange<T>();
        }
   
        bool result = _onValueChange(self, _last);
        _last = _onCloneObject(self);
        return result;
    }
    
    static Func<TSource, TSource> CloneObj<TSource>()
    {
        //Parameter (Type type, string name) 创建一个 ParameterExpression 节点，该节点可用于标识表达式树中的参数或变量
        //type 参数或变量的类型。
        //name 仅用于调试或打印目的的参数或变量的名称。
        var p = Expression.Parameter(typeof(TSource));
        var r = Expression.Variable(typeof(TSource), "result");
        //创建一个表示赋值运算的 BinaryExpression。
        var letr = Expression.Assign(r, Expression.New(typeof(TSource).GetConstructor(Type.EmptyTypes)));

        var copyexprs = typeof(TSource).GetProperties()
            .Join(typeof(TSource).GetProperties()//Join 基于匹配键对两个序列的元素进行关联。使用默认的相等比较器对键进行比较
                , x => x.Name
                , y => y.Name
                , (x, y) => new { x, y })
            .Select(x =>
            {
                var temp = Expression.Assign(
                    Expression.MakeMemberAccess(r, x.y), //创建一个表示访问字段或属性的 MemberExpression。
                    Expression.MakeMemberAccess(p, x.x));
                return temp;
            }).ToList();
        //块表达式
        var block = Expression.Block(
            //添加局部变量
            new[] { r },
            new Expression[] { r, letr }
                .Concat(copyexprs)
                .Concat(new Expression[] { r })
                .ToArray());

        var lambda = Expression.Lambda(block, p);

        return lambda.Compile() as Func<TSource, TSource>;
    }
    
    private static Func<TSource, TSource, bool> ValueChange<TSource>()
    {
        var p = Expression.Parameter(typeof(TSource));
        var r = Expression.Parameter(typeof(TSource));
        var result = Expression.Variable(typeof(bool), "result");
        LabelTarget labelTarget = Expression.Label(typeof(bool));

        var init = Expression.Assign(result, Expression.Constant(false, typeof(bool)));
        var array = typeof(TSource).GetProperties().Select(u =>
        {
            var test = Expression.NotEqual(
                Expression.MakeMemberAccess(r, u), //创建一个表示访问字段或属性的 MemberExpression。
                Expression.MakeMemberAccess(p, u));
            var ifTrue = Expression.Block(
                Expression.Assign(result, Expression.Constant(true, typeof(bool))),
                Expression.Return(labelTarget, result),
                Expression.Label(labelTarget, Expression.Constant(false)));
            //Expression.Assign(result, Expression.Constant(false, typeof(bool)));
            return Expression.IfThen(test, ifTrue);
        }).ToArray();


        //块表达式
        var block = Expression.Block(
            new[] { result },
            new Expression[] { result, init }
                .Concat(array)
                .Concat(new Expression[] { result })
                .ToArray());

        var lambda = Expression.Lambda(block, r, p);

        return lambda.Compile() as Func<TSource, TSource,bool>;
    }
}