
require("./Keys");

global.InitChatRoom = function(socket)
{
    socket.on(Keys.Chat, function (data) {
        //console.log("SocketProtocol.Chat:"+data);

        var chatContent ={};
        chatContent.id      = data.id;
        chatContent.chatMessage  = data.chatMessage;

        console.log("chatContent.toJSON():"+JSON.stringify(chatContent));
        //socket.emit(Keys.ReceiveChat,chatContent);//All SocketUsers
        socket.broadcast.emit(Keys.ReceiveChat,chatContent);//All SocketUsers But Self
    });
};