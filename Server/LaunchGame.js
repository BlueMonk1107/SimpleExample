var io = require('socket.io')(3000);
var login = require('./Login');
require("./Keys");

console.log("Start Server");
io.on(Keys.Connection,function(socket)
{
    console.log("client connection");
    login.InitLogin(socket);
    socket.emit("connection");
});