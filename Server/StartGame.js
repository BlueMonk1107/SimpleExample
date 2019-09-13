var chatRoom = require('./ChatRoom');

let players = {};

module.exports.InitStartGame = function (socket, playerId) {

    let player = {id:playerId,postion:{x:0,y:0,z:0},rotation:{x:0,y:0,z:0}};

    socket.on(Keys.InitGameComplete, function (data) {
        console.log("Init StartGame");

        chatRoom.InitChatRoom(socket);

        
        players[playerId] = player;
        //生成本客户端的所有对象
        for(let id in players)
        {
            socket.emit(Keys.Spawn,players[id]);
        }
        //通知其他客户端，生成本客户端的对象
        socket.broadcast.emit(Keys.Spawn, player);
    })

    socket.on(Keys.Move,function(data)
    {
        player.postion.x= data.startPos.x;
        player.postion.y= data.startPos.y;
        player.postion.z= data.startPos.z;

        socket.emit(Keys.Move,data);
        socket.broadcast.emit(Keys.Move,data);
    })

    socket.on(Keys.Disconnect,function(data)
    {
        delete players[playerId];
        socket.broadcast.emit(Keys.OtherDisconnect,{id:playerId});
    })
};