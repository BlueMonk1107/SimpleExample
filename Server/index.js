/*!
 * express
 * Copyright(c) 2009-2013 TJ Holowaychuk
 * Copyright(c) 2013 Roman Shtylman
 * Copyright(c) 2014-2015 Douglas Christopher Wilson
 * MIT Licensed
 */

'use strict';

var port = process.env.PORT || 3000;
var io = require('socket.io')(port);
console.log('Server Start....port:'+port);



var socketArray = {};//Socket ����

io.sockets.on('connection', function (socket) {
    console.log('Client Contect. SocketID:'+socket.id);
    //socket.emit('SocketProtocol.Info', { hi: 'Hello,world' });
    socket.emit('SocketProtocol.Info', "ConnectServer Scuess! :"+socket.id);
    socket.on('SocketProtocol.Login', function (data) {
        console.log(data);

        //var content = JSON.parse(data);
        socket.nickName =  data.nickName;
        socket.guid      = data.guid;
        //
        if( !socketArray.hasOwnProperty(socket.guid) )
        {
            socketArray[socket.guid] = data;
        }
        //
        //console.log(socketArray);
        var chatContent={};
        chatContent.nickName        =  socket.nickName;
        chatContent.chatMessage     = "Login Scuess";

        socket.emit('SocketProtocol.Login',chatContent);


    });

    socket.on('SocketProtocol.Chat', function (data) {
        //console.log("SocketProtocol.Chat:"+data);

        var chatContent ={};
        chatContent.loginName      = socket.loginName;
        chatContent.chatMessage  = data.chatMessage;

        console.log("chatContent.toJSON():"+JSON.stringify(chatContent));
        io.sockets.emit('SocketProtocol.Chat',chatContent);//All SocketUsers
        //socket.broadcast.emit(data);//All SocketUsers But Self
    });

    socket.on('disconnect', function () {
        console.log("disconnect");
        if(  socketArray.hasOwnProperty(socket.guid) )
       {
           delete socketArray[socket.guid];
           console.log("disconnect Delete:"+socket.guid);
       }
        console.log("Inline Count:"+countProperties(socketArray));
    });

});

function countProperties (obj) {
    var count = 0;
    for (var property in obj) {
        if (Object.prototype.hasOwnProperty.call(obj, property)) {
            count++;
        }
    }
    return count;
}

this.size = function () {
    var count = 0;
    for (var prop in items) {
        if (items.hasOwnProperty(prop)) {
            ++count;
        }
    }
    return count;
};  ;