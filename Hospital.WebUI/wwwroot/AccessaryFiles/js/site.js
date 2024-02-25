"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

connection.start().then(function () {
    //GetAllUsers();
    console.log("Connected");
}).catch(function (err) {
    return console.error(err.toString());
})

connection.on("Connect", function (role) {
<<<<<<< HEAD
    //if (role == "admin") {
    //console.log(role);
    //}
    GetAllPatients();
    GetAllAppointments();
    GetDays();
    GetAllDoctors();
    GetAllDepartment(); 
    //GetDay();
    //GetTime();
=======
    if (role == "admin") {
        //console.log(role);
        GetAllPatients();
        GetAllAppointments();
        GetAllDoctors();
        GetAllDepartment();
        GetAllPost();
    }
>>>>>>> bf12e164ee3a684225be48c6f4225e079ee9b71e

    //GetAllUsers();
    //element.style.display = "block";
    //element.innerHTML = info;
    //setTimeout(() => {
    //    element.innerHTML = "";
    //    element.style.display = "none";
    //}, 5000);
})

async function AdminCall(id) {
    await connection.invoke("AdminCall", id);
}

connection.on("AdminRefresh", function (id) {
    GetAllPatients();
    GetAllAppointments();
    //GetMyRequests();
    //GetAllUsers();
    //GetMyAndFriendPosts();
})