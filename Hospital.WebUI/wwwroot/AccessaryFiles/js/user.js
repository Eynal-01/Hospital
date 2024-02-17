var departmentName = "";

function GetAllPatients() {
    /*    console.log("dsdsdd");*/
    $.ajax({
        url: `/Admin/AllPatients`,
        method: "GET",
        success: function (data) {
            let content = "";
            let name = "";

            for (var i = 0; i < data.length; i++) {
                if (data[i].fullName == null || data[i].fullName == "") {
                    name = `
                    
                     <td>${data[i].userName}</td>

                    `;
                }
                else {
                    name = `

                     <td>${data[i].fullName}</td>

                    `;
                }
                content += `
                 <tr>
                     <td><span class="list-icon"><img class="patients-img" src="/AccessaryFiles/images/${data[i].avatart}" alt=""></span></td>
                     <td><span class="list-name">${data[i].id}</span></td> 
                     ${name}
                     <td>${data[i].age}</td>
                     <td>${data[i].address}</td>
                     <td>${data[i].phoneNumber}</td>
                     <td>11 Jan 2018</td>
                     <td><span class="badge badge-success">Approved</span></td>
                 </tr>
                
                `;
            }
            $("#patients").html(content);
        }
    })
}

function GetAllAppointments() {
    //console.log("GetAllAppointments work");
    $.ajax({
        url: `/Admin/ShowAllAppointments`,
        method: "GET",

        success: function (data) {
            let content = "";

            for (var i = 0; i < data.length; i++) {
                //var doctor = GetAppointmentDoctor(data[i].doctorId)
                content += `
                  <tr>
                      <td>${data[i].id}</td>
                      <td>${data[i].appointmentDate} ${data[i].appointmentTime}</td>
                      <td>${data[i].patient.userName}</td>
                      <td>32</td>
                      <td>${data[i].doctor.firstName} ${data[i].doctor.lastName}</td>
                      <td>${data[i].department.departmentName}</td>
                 </tr>`;
            }
            $("#appointments").html(content);
        }
    })
}

<<<<<<< HEAD
function GetDays() {
    var availablecount = $("#availableDay").value;
    $.ajax({
        url: `/Admin/GetAvailableDays?availableCount=${availablecount}`,
        method: "GET",

        success: function (data) {
            GetDa(availablecount);
        }
    })
}

function GetDa(availablecount) {
    $.ajax({
        url: `/Home/GetAvailableDays?availableCount=${availablecount}`,
        method: "GET",

        success: function (data) {
            var content = "";

            for (var i = 0; i < data.length; i++) {
                content += `            
                     <option value="${data[i]}">${data[i]}</option>
                `;
            }
            $("#exampleFormControlSelect2").html(content);
=======
function GetAllDoctors() {
    //console.log("doctor");
    $.ajax({
        url: `/Admin/GetAllDoctors`,
        method: "GET",

        success: function (data) {
            let content = "";

            for (var i = 0; i < data.length; i++) {
                //var doctor = GetAppointmentDoctor(data[i].doctorId)
                var department = "";
                //GetDoctorIdDepartment(data[i].id);
                content += `
                            <div class="col-lg-3 col-md-4 col-sm-6">
                                <div class="card xl-blue member-card doctor">
                                    <div class="body">
                                        <div class="member-thumb">
                                            <img src="/AccessaryFiles/images/${data[i].avatar}" class="img-fluid" alt="profile-image">
                                        </div>
                                        <div class="detail">
                                            <h4 class="m-b-0">Dr. ${data[i].userName}</h4>
                                            <ul class="social-links list-inline m-t-20">
                                                <li><a title="facebook" href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                                <li><a title="twitter" href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                                <li><a title="instagram" href="#"><i class="zmdi zmdi-instagram"></i></a></li>
                                            </ul>
                                            <a href='/Admin/DoctorProfile?doctorId=${data[i].id}'  class="btn btn-default btn-round btn-simple" >View Profile</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            `
            }
            $("#doctors").html(content);
>>>>>>> c1fbed677263c2504a18112ba2cf435234fe9d57
        }
    })
}

<<<<<<< HEAD

=======
                                            //<p class="text-muted">${departmentName}</p>
//function GetDoctorIdDepartment(doctorId) {
//    $.ajax({
//        url: `/Admin/GetDoctorIdDepartment?doctorId=${doctorId}`,
//        method: "GET",

//        success: function (department) {
//            departmentName = department;
//        }
//    })
//}
>>>>>>> c1fbed677263c2504a18112ba2cf435234fe9d57

//function GetAppointmentDoctor() {
//    $.ajax({
//        url: `/Admin/GetAppointmentDoctor`,
//        method: "GET",

//        success: function (data) {
//            return data;
//        }
//    })
//}