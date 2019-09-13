
module.exports.InitChatRoom = function(socket)
{
    socket.on(Keys.Chat,function(data){
        console.log("chat id:"+data.id);
        console.log("socket id:"+socket.id);
        socket.id = 0;
        socket.emit(Keys.ReceiveChat,data);
        socket.broadcast.emit(Keys.ReceiveChat,data);
    })
};