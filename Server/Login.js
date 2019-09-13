
var startGame = require("./StartGame");

var testAccount = [{ id: "111", password: "111" }, { id: "222", password: "222" }];

module.exports.InitLogin = function (socket) {
    console.log("Init Login");
    socket.on(Keys.Login, function (data) {
        console.log(data.id);
        var result = {};
        result.id = data.id;
        result.result = CheckAccount(data);
        socket.emit(Keys.Login, result);

        if(result.result == true)
        {
            startGame.InitStartGame(socket,data.id);
        }
    });
};


function CheckAccount(data) {

    var result = false;
    testAccount.forEach(function (item) {
        if(item.id == data.id && item.password == data.password)
        {
            result = true;
        }
    });

    return result;
}