
global.InitChatRoom = function(socket)
{
    socket.on('chat', function (data) {
        //console.log("SocketProtocol.Chat:"+data);

        var chatContent ={};
        chatContent.id      = socket.id;
        chatContent.chatMessage  = data.chatMessage;

        console.log("chatContent.toJSON():"+JSON.stringify(chatContent));
        socket.emit('chat',chatContent);//All SocketUsers
        //socket.broadcast.emit(data);//All SocketUsers But Self
    });
};