
function GetAllPatients() {
    console.log("dsdsdd");
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
    console.log("GetAllAppointments work");
    $.ajax({
        url: `/Admin/ShowAllAppointments`,
        method: "GET",

        success: function (data) {
            let content1 = "";

            for (var i = 0; i < data.length; i++) {

                content1 += `
                  <tr>
                      <td>${data[i].id}</td>
                      <td>${data[i].appointmentDate}</td>
                      <td>${data[i].patientId}</td>
                      <td>32</td>
                      <td>${data[i].doctorId}</td>
                      <td>${data[i].departmentId}</td>
                 </tr>`;
            }
            $("appointments").html(content);
        }
    })
}