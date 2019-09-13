var port = process.env.PORT || 3000;
var io = require('socket.io')(port);
//require("./Keys");
require("./Login");

console.log("Server started on port " + port);

io.on(Keys.Connection, function (socket) {
    console.log("client connect");
    socket.emit(Keys.Connection);
    InitLogin(socket);
});