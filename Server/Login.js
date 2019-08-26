
require("./StartGame");
require("./Keys");
var socketArray = {};

global.InitLogin = function (socket) {
    console.log("login init");
    socket.on(Keys.Login, function (data) {
        socket.id = data.id;
        socket.loginPassword = data.loginPassword;
        //
        // if (!socketArray.hasOwnProperty(socket.guid)) {
        //     socketArray[socket.guid] = data;
        // }
        //
        //console.log(socketArray);
        var chatContent = {};
        chatContent.id = socket.id;
        chatContent.chatMessage = "Login Scuess";

        socket.emit(Keys.Login, chatContent);
        console.log("Login Scuess id:"+data.id);
        InitStartGame(socket, data.id);
    })
}