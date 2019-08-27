require("./ChatRoom");

var players = {};
var playerSpeed = 5;

global.InitStartGame = function (socket,playerId)
{
    console.log("InitStartGame");

    var player = {
        id:playerId,
        destination:{
        x:0,
        y:0    
        },
        lastPosition:{
            x:0,
            y:0
        },
        lastMoveTime : 0
    };

    socket.on(Keys.InitGameComplete,function (data){
        console.log("InitGameComplete");
        InitChatRoom(socket);
       
        players[playerId] = player;
    
        socket.emit('register', {id:playerId});
        //socket.emit('spawn', player);
        socket.broadcast.emit('spawn', player);
        socket.broadcast.emit('requestPosition');

        for(var id in players){
            console.log(id);
            if(id == playerId)
                continue;
            socket.emit('spawn', players[id]);
        };
    });
    
    socket.on('move', function (data) {
        console.log(data.id);
        data.id = playerId;
        console.log('client moved', JSON.stringify(data));
        
        player.destination.x = data.d.x;
        player.destination.y = data.d.y;
        
        var elapsedTime = Date.now() - player.lastMoveTime;
        
        var travelDistanceLimit = elapsedTime * playerSpeed / 1000;
        
        var requestedDistanceTraveled = lineDistance(player.lastPosition, data.c);
        
        player.lastMoveTime = Date.now();
        
        player.lastPosition = data.c;
        
        delete data.c;
        
        data.x = data.d.x;
        data.y = data.d.y;
        
        delete data.d;
        
        socket.broadcast.emit('move', data);
    });
    
     socket.on('follow', function (data) {
        data.id = playerId;
        console.log("follow request: ", data);
        socket.broadcast.emit('follow', data);
    });
    
    socket.on('updatePosition', function (data) {
        console.log("update position: ", data);
        data.id = playerId;
        socket.broadcast.emit('updatePosition', data);
    });
    
    socket.on('attack', function (data) {
        console.log("attack request: ", data);
        data.id = playerId;
        io.emit('attack', data);
    });
    
    socket.on('disconnect', function () {
        console.log('client disconected');
        delete players[playerId];
        socket.broadcast.emit('disconnected', {id:playerId});
    });
}

function lineDistance(vectorA, vectorB) {
    var xs = 0;
    var ys = 0;
    
    xs = vectorB.x - vectorA.x;
    xs = xs * xs;
    
    ys = vectorB.y - vectorA.y;
    ys = ys * ys;
    
    return Math.sqrt(xs + ys);
}