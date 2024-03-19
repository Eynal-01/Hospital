"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/userhub").build();

connection.start().then(function () {
    //GetAllUsers();
    console.log("Connected");
}).catch(function (err) {
    return console.error(err.toString());
})

connection.on("Connect", function (role) {
    //console.log("df");
    if (role === "admin") {
        GetAllPatients();
        GetAllAppointments();
        GetAllDoctors();
    }
    GetAllDepartment();
    //else if (role == "patient") {
    //    GetAllPostPatient();
    //}
    //if (role == "doctor") {
    //    console.log(role);
    GetAllPostAllUsers();
    //}
    //else if (role == "admin") {
    //    console.log(role);
    //    GetAllPostAdmin();
    //}

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

async function DoctorCall(id) {
    await connection.invoke("DoctorCall", id);
}

function GetMessageLiveChatCall(id, id2) {
    //console.log(id);
    //console.log(id2);
    //console.log("Received sender: " + id);
    //console.log("Received receiver: " + id2);
    connection.invoke("LiveChatCall", id, id2);
}

connection.on("AdminRefresh", function (id) {
    GetAllPatients();
    GetAllAppointments();
    //GetMyRequests();
    //GetAllUsers();
    //GetMyAndFriendPosts();
})

connection.on("DoctorPostShow", function (id) {
    GetAllPostDoctor();
    //GetMyRequests();
    //GetAllUsers();
    //GetMyAndFriendPosts();
})

connection.on("ReceiveMessage", function (id) {
    //console.log("Received message: " + message);
    UserMessage(id);
});

//connection.on("LiveChat", function (id) {
//    //console.log("sadads");
//    UserMessage(id);
//})
