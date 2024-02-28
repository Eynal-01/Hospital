﻿var departmentName = "";
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
                 <tr onclick="PatientProfile('${data[i].id}')">
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


function PatientProfile(id) {
    $.ajax({
        url: `/Admin/PatientProfile/${id}`,
        method: "GET",

        success: function (data) {
            console.log("s");
        }
    })
}

//function UserRefresh() {
//    $.ajax({
//        url: `/Admin/GetAll/${id}`,
//        method: "GET",

//        success: function (data) {
//            console.log("s");
//        }
//    })
//}

function GetAllPostAllUsers() {
    //var queryControllerName = "";
    //if (role == "admin") {
    //    queryControllerName = "Admin";
    //}
    //else {
    //    queryControllerName = "Doctor";
    //}
    $.ajax({
        url: `/Post/GetAllPost`,
        method: "GET",

        success: function (data) {
            console.log(data);
            var content = "";
            var adminName = "";
            var arrow = "";
            var images = "";

            for (var i = 0; i < data.posts.length; i++) {
                images = "";
                arrow = "";

                for (var k = 0; k < data.posts[i].images.length; k++) {
                    if (k == 0) {

                        images += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid"  src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image" >
                            </div>
                       `;
                    }
                    else {
                        images += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image" >
                          </div>
                       `;
                    }
                }

                if (data.posts[i].images.length > 1) {
                    arrow += `
                         <a class="carousel-control-prev" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="prev">
                           <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                           <span class="sr-only">Previous</span>
                         </a>
                         <a class="carousel-control-next" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="next">
                           <span class="carousel-control-next-icon" aria-hidden="true"></span>
                           <span class="sr-only">Next</span>
                         </a>
                    `;
                }



                if (data.posts[i].lastName != null && data.posts[i].firstName != null) {
                    adminName = `
                           <ul class="meta">
                                <li><a href="#"><i class="zmdi zmdi-account col-blue"></i>Posted By: ${data.posts[i].admin.firstName} ${data.posts[i].admin.lastName}</a ></li >
                           </ul>
                    `;
                }

                content += `

                   <div class="card single_post">
                        <div class="body">
                            <h3 class="m-t-0 m-b-5"><a href="blog-details.html">${data.posts[i].title}</a></h3>
                            ${adminName}
                        </div>
                        <div class="body">
                            <div class="img-post m-b-15">
                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>
                            <p>${data.posts[i].content}</p>
                            <a href="blog-details.html" title="read more" class="btn btn-round btn-info">Read More</a>
                        </div>
                    </div>
                
                `;
            }

            $("#postsDoctor").html(content);
            $("#posts").html(content);
        }
    })
}

//function GetAllPostAdmin() {
//    //var queryControllerName = "";
//    //if (role == "admin") {
//    //    queryControllerName = "Admin";
//    //}
//    //else {
//    //    queryControllerName = "Doctor";
//    //}
//    $.ajax({
//        url: `/Admin/GetAllPost`,
//        method: "GET",

//        success: function (data) {
//            console.log(data);
//            var content = "";
//            var adminName = "";
//            var arrow = "";
//            var images = "";

//            for (var i = 0; i < data.posts.length; i++) {
//                images = "";
//                arrow = "";

//                for (var k = 0; k < data.posts[i].images.length; k++) {
//                    if (k == 0) {

//                        images += `
//                             <div class="carousel-item active" style="text-align:center;width:100%;">
//                               <img class="img-fluid"  src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image" >
//                            </div>
//                       `;
//                    }
//                    else {
//                        images += `
//                          <div class="carousel-item" style="text-align:center;width:100%;">
//                               <img class="img-fluid" src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image" >
//                          </div>
//                       `;
//                    }
//                }

//                if (data.posts[i].images.length > 1) {
//                    arrow += `
//                         <a class="carousel-control-prev" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="prev">
//                           <span class="carousel-control-prev-icon" aria-hidden="true"></span>
//                           <span class="sr-only">Previous</span>
//                         </a>
//                         <a class="carousel-control-next" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="next">
//                           <span class="carousel-control-next-icon" aria-hidden="true"></span>
//                           <span class="sr-only">Next</span>
//                         </a>
//                    `;
//                }



//                if (data.posts[i].lastName != null && data.posts[i].firstName != null) {
//                    adminName = `
//                           <ul class="meta">
//                                <li><a href="#"><i class="zmdi zmdi-account col-blue"></i>Posted By: ${data.posts[i].admin.firstName} ${data.posts[i].admin.lastName}</a ></li >
//                           </ul>
//                    `;
//                }

//                content += `

//                   <div class="card single_post">
//                        <div class="body">
//                            <h3 class="m-t-0 m-b-5"><a href="blog-details.html">${data.posts[i].title}</a></h3>
//                            ${adminName}
//                        </div>
//                        <div class="body">
//                            <div class="img-post m-b-15">
//                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
//                                   <div class="carousel-inner">
//                                     ${images}
//                                   </div>
//                                   ${arrow}
//                               </div>
//                            </div>
//                            <p>${data.posts[i].content}</p>
//                            <a href="blog-details.html" title="read more" class="btn btn-round btn-info">Read More</a>
//                        </div>
//                    </div>
                
//                `;
//            }
//            DoctorShowPost();
//        }
//    })
//}

function DoctorShowPost() {
    $.ajax({
        url: `/Admin/DoctorShowPost`,
        method: "GET",

        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                DoctorCall(data[i].id);
            }
        }
    })
}

function GetAllPostPatient() {
    $.ajax({
        url: `/Home/GetAllPost`,
        method: "GET",

        success: function (data) {
            let content = "";
            var images = "";
            var arrow = "";

            for (var i = 0; i < data.posts.length; i++) {
                //var doctor = GetAppointmentDoctor(data[i].doctorId)
                images = "";
                arrow = "";

                for (var k = 0; k < data.posts[i].images.length; k++) {
                    if (k == 0) {
                        images += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image">
                            </div>
                       `;
                    }
                    else {
                        images += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="/AccessaryFiles/images/${data.posts[i].images[k]}" alt="Responsive image">
                          </div>
                       `;
                    }
                }

                if (data.posts[i].images.length > 1) {
                    arrow += `
                         <a class="carousel-control-prev" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="prev">
                           <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                           <span class="sr-only">Previous</span>
                         </a>
                         <a class="carousel-control-next" href="#carouselExampleIndicators${data.posts[i].postId}" role="button" data-slide="next">
                           <span class="carousel-control-next-icon" aria-hidden="true"></span>
                           <span class="sr-only">Next</span>
                         </a>
                    `;
                }

                content += `
                      <div class="col-lg-12 col-md-12 mb-5">
							<div class="blog-item">
                              <div class="img-post m-b-15">
                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>

								<div class="blog-item-content">
									<div class="blog-item-meta mb-3 mt-4">
										<span class="text-black text-capitalize mr-3"><i class="icofont-calendar mr-1"></i> 28th January</span>
									</div>

									<h2 class="mt-3 mb-3"><a href="blog-single.html">${data.posts[i].title}</a></h2>

									<p class="mb-4">${data.posts[i].content}</p>

									<a href="blog-single.html" target="_blank" class="btn btn-main btn-icon btn-round-full">Read More <i class="icofont-simple-right ml-2  "></i></a>
								</div>
							</div>
						</div>
                `;
            }
            $("#patientPosts").html(content);
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

function GetAllDepartment() {
    $.ajax({
        url: `/Admin/GetAllDepartment`,
        method: "GET",

        success: function (data) {
            let content = "";
            content += `
                  <li><a href="/Admin/AddDepartment">Add</a></li>
                  <li><a href="/Admin/AllDepartments">All Departments</a></li>
            `;
            for (var i = 0; i < data.length; i++) {
                //var doctor = GetAppointmentDoctor(data[i].doctorId)
                content += `
                                <li><a href="javascript:void(0);">${data[i].departmentName}</a></li>

`;
            }
            $("#departments").html(content);
        }
    })
}

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
        }
    })
}

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

//function GetAppointmentDoctor() {
//    $.ajax({
//        url: `/Admin/GetAppointmentDoctor`,
//        method: "GET",

//        success: function (data) {
//            return data;
//        }
//    })
//}