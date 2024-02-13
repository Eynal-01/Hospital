"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

connection.start().then(function (ro) {
    //GetAllUsers();

    console.log(ro);
    //if (data == "admin") {
    GetAllPatients();
    //}
    console.log("Connected");
}).catch(function (err) {
    return console.error(err.toString());
})

connection.on("Connect", function (info) {
    console.log("Connect Work");
    //GetAllUsers();
    //element.style.display = "block";
    //element.innerHTML = info;
    //setTimeout(() => {
    //    element.innerHTML = "";
    //    element.style.display = "none";
    //}, 5000);
})