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

function PostFilter(departmentId) {
    $.ajax({
        url: `/Post/PostFilter?departmentId=${departmentId}`,
        method: "GET",

        success: function (data) {
            var contentPatient = "";
            var adminName = "";
            var arrowPatient = "";
            var imagesPatient = "";
            //console.log(data.value);
            for (var i = 0; i < data.value.posts.length; i++) {
                imagesPatient = "";
                arrowPatient = "";
                adminName = "";

                for (var k = 0; k < data.value.posts[i].images.length; k++) {
                    if (k == 0) {
                        imagesPatient += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.value.posts[i].images[k]}" alt="Responsive image">
                            </div>
                       `;
                    }
                    else {
                        imagesPatient += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.value.posts[i].images[k]}" alt="Responsive image">
                          </div>
                       `;
                    }
                }

                if (data.value.posts[i].images.length > 1) {
                    arrowPatient += `
                         <a class="carousel-control-prev" href="#carouselExampleIndicators${data.value.posts[i].postId}" role="button" data-slide="prev">
                           <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                           <span class="sr-only">Previous</span>
                         </a>
                         <a class="carousel-control-next" href="#carouselExampleIndicators${data.value.posts[i].postId}" role="button" data-slide="next">
                           <span class="carousel-control-next-icon" aria-hidden="true"></span>
                           <span class="sr-only">Next</span>
                         </a>
                    `;
                }



                if (data.value.posts[i].lastName != null && data.value.posts[i].firstName != null) {
                    adminName = `
                           <ul class="meta">
                                <li><a href="#"><i class="zmdi zmdi-account col-blue"></i>Posted By: ${data.value.posts[i].admin.firstName} ${data.value.posts[i].admin.lastName}</a ></li >
                                <li><a href="#"><i class="zmdi zmdi-label col-red"></i>${data.value.posts[i].department.departmentName}</a></li>
                           </ul>
                    `;
                }
                else {
                    adminName = `
                           <ul class="meta">
                                <li><a href="#"><i class="zmdi zmdi-account col-blue"></i>Posted By: ${data.value.posts[i].admin.userName}</a ></li >
                                <li><a href="#"><i class="zmdi zmdi-label col-red"></i>${data.value.posts[i].department.departmentName}</a></li>
                           </ul>
                    `;
                }


                if (data.value.posts[i].images.length > 1) {
                    arrowPatient += `
                         <a class="carousel-control-prev" href="#carouselExampleIndicators${data.value.posts[i].postId}" role="button" data-slide="prev">
                           <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                           <span class="sr-only">Previous</span>
                         </a>
                         <a class="carousel-control-next" href="#carouselExampleIndicators${data.value.posts[i].postId}" role="button" data-slide="next">
                           <span class="carousel-control-next-icon" aria-hidden="true"></span>
                           <span class="sr-only">Next</span>
                         </a>
                    `;
                }

                contentPatient += `
                      <div class="col-lg-12 col-md-12 mb-5">
							<div class="blog-item">
                              <div class="img-post m-b-15" style="background-color:rgba(200, 200, 200,0.4);">
                                 <div id="carouselExampleIndicators${data.value.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${imagesPatient}
                                   </div>
                                   ${arrowPatient}
                               </div>
                            </div>

								<div class="blog-item-content">
									<div class="blog-item-meta mb-3 mt-4">
                                        <span class="text-muted text-capitalize mr-3"><i class="icofont-category mr-2"></i> ${data.value.posts[i].department.departmentName}</span>
										<span class="text-black text-capitalize mr-3"><i class="icofont-calendar mr-1"></i> ${data.value.posts[i].publishTime}</span>
									</div>

									<h2 class="mt-3 mb-3"><a href="blog-single.html">${data.value.posts[i].title}</a></h2>

									<p class="mb-4">${data.value.posts[i].content}</p>

									<a href="PostSingle('${data.value.posts[i].id}')" target="_self" class="btn btn-main btn-icon btn-round-full">Read More <i class="icofont-simple-right ml-2  "></i></a>
								</div>
							</div>
						</div>
                `;
            }

            $("#patientPosts").html(contentPatient);
        }
    })
}

function PostSingle(postId) {
    $.ajax({
        url: `/Post/BlogSingle?postId=${postId}`,
        method: "GET",

        success: function (data) {
            BlogSingle(data);
        }
    })
}

function BlogSingle(post) {
    console.log(post);
    $.ajax({
        url: `/Home/BlogSingle`,
        method: "GET",
        data: post,
        success: function (data) {
            console.log("Df");
        }
    })
}

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
            //console.log(data);
            var content = "";
            var adminName = "";
            var arrow = "";
            var images = "";

            let contentPatient = "";
            var imagesPatient = "";
            var categoryList = [];
            var arrowPatient = "";

            var postCategories = "";

            var postCategoriessInCount = 0;

            imagesPatient = "";
            arrowPatient = "";

            postCategories += ` 
                      <li class="align-items-center">
					     <a onclick="PostFilter('All')">All</a>
					     <span>(${data.posts.length})</span>
					  </li>
               `;

            for (var i = 0; i < data.posts.length; i++) {
                postCategoriessInCount = 0;

                for (var k = 0; k < data.posts.length; k++) {
                    if (data.posts[k].department.id === data.posts[i].department.id) {
                        postCategoriessInCount += 1;
                    }
                }

                if (!categoryList.includes(data.posts[i].department.departmentName)) {
                    categoryList.push(data.posts[i].department.departmentName);

                    postCategories += ` 
                              <li class="align-items-center">
				          	     <a onclick="PostFilter('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCount})</span>
				          	  </li>
                          `;
                }
            }

            for (var i = 0; i < data.posts.length; i++) {
                images = "";
                arrow = "";
                imagesPatient = "";

                for (var k = 0; k < data.posts[i].images.length; k++) {
                    if (k == 0) {

                        images += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid"  src="${data.posts[i].images[k]}" alt="Responsive image" >
                            </div>
                       `;

                        imagesPatient += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.posts[i].images[k]}" alt="Responsive image">
                            </div>
                       `;
                    }
                    else {
                        images += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.posts[i].images[k]}" alt="Responsive image" >
                          </div>
                       `;

                        imagesPatient += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.posts[i].images[k]}" alt="Responsive image">
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
                                <li><a href="#"><i class="zmdi zmdi-label col-red"></i>${data.posts[i].department.departmentName}</a></li>
                           </ul>
                    `;
                }
                else {
                    adminName = `
                           <ul class="meta">
                                <li><a href="#"><i class="zmdi zmdi-account col-blue"></i>Posted By: ${data.posts[i].admin.userName}</a ></li >
                                <li><a href="#"><i class="zmdi zmdi-label col-red"></i>${data.posts[i].department.departmentName}</a></li>
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
                            <a target="_self"  title="read more" class="btn btn-round btn-info">Read More</a>
                        </div>
                    </div>
                
                `;

                if (data.posts[i].images.length > 1) {
                    arrowPatient += `
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

                contentPatient += `
                      <div class="col-lg-12 col-md-12 mb-5">
							<div class="blog-item">
                              <div class="img-post m-b-15" style="background-color:rgba(200, 200, 200,0.4);">
                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${imagesPatient}
                                   </div>
                                   ${arrowPatient}
                               </div>
                            </div>

								<div class="blog-item-content">
									<div class="blog-item-meta mb-3 mt-4">
                                        <span class="text-muted text-capitalize mr-3"><i class="icofont-category mr-2"></i> ${data.posts[i].department.departmentName}</span>
										<span class="text-black text-capitalize mr-3"><i class="icofont-calendar mr-1"></i> ${data.posts[i].publishTime}</span>
									</div>

									<h2 class="mt-3 mb-3"><a href="blog-single.html">${data.posts[i].title}</a></h2>

									<p class="mb-4">${data.posts[i].content}</p>

									<a href="/Post/BlogSingle?postId=${data.posts[i].postId}" target="_self" class="btn btn-main btn-icon btn-round-full">Read More <i class="icofont-simple-right ml-2  "></i></a>
								</div>
							</div>
						</div>
                `;
            }

            $("#patientPosts").html(contentPatient);
            $(".postFilterCategories").html(postCategories);


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

function GetDays() {
    var availablecount = $("#availableDay").val();
    $.ajax({
        url: `/Admin/GetAvailableDays?availableCount=${availablecount}`,
        method: "GET",

        success: function (data) {
            console.log("GetDays success")
        }
    })
}

function GetDay() {
    var availableDoctor = $("#doctorSelect").val();
    console.log("GetDay CALLED");
    $.ajax({
        url: `/Home/GetAvailableDays?doctorId=${availableDoctor}`,
        method: "GET",
        success: function (data) {
            console.log(data)
            var content = "";

            for (var i = 0; i < data.length; i++) {
                content += `
                     <option value="${data[i]}">${data[i]}</option>`;
            }
            $("#exampleFormControlSelect3").html(content);
        }
    });
}


function GetTime() {
    var availableDoctor = $("#doctorSelect").val();
    var appointmentDate = $("#exampleFormControlSelect3").val();
    console.log("Time CALLED");
    $.ajax({
        url: `/Home/GetAvailableTimes?doctorId=${availableDoctor}&appointmentDate=${appointmentDate}`,
        method: "GET",
        success: function (data) {
            console.log(data)
            var content = "";

            for (var i = 0; i < data.length; i++) {
                content += `
                     <option value="${data[i]}">${data[i]}</option>`;
            }
            $("#exampleFormControlSelect4").html(content);
        }
    });
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

//function SendSMS() {
//    $.ajax({
//        url: `/SendSMS/SendText`,
//        method: "POST",
//        success: function (data) {
//            console.log(data);
//        }
//    })
//}

function SendSMS() {
    var phoneNumber = $("#phone").val();
    $.ajax({
        url: `/SendSMS/SendText`,
        method: 'POST',
        success: function (data) {
            console.log('Message sent:', data.messageSid);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

document.getElementById("departmentSelect").addEventListener("change", function () {
    var departmentId = this.value;

    var doctorSelect = document.getElementById("doctorSelect");
    doctorSelect.innerHTML = "";

    $.ajax({

        url: `/Home/getDoctors?departmentId=${departmentId}`,
        method: "GET",

        success: function (data) {
            console.log(data);
            data.forEach(function (doctor) {
                var option = document.createElement("option");
                option.text = doctor.firstName + " " + doctor.lastName;
                option.value = doctor.id;
                doctorSelect.appendChild(option);
            });
            GetDay();
        }
    })
});

document.getElementById("doctorSelect").addEventListener("change", function () {
    GetDay();
    GetTime();
});

var fullName = $("#name").val();
var phone = $("#phone").val();
var btn = $("#make");
var emptyPhone = document.getElementById("emptyPhone");
var emptyName = document.getElementById("emptyName");
var n = document.getElementById("name");
var p = document.getElementById("phone");
/*function checkInputs() {*/
document.getElementById("make").addEventListener("click", function () {
    $.ajax({
        url: `/Home/CheckInputs?phoneNumber=${p.value}&fullName=${n.value}`,
        method: "GET",
        success: function (data) {

            if (data.includes("fullname is null")) {
                n.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
                emptyName.style.display = "inline-block";
            }
            if (data.includes("phone is null")) {
                p.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
                emptyPhone.style.display = "inline-block";
                console.log("phone is null");
            }

            if (data === "") {
                document.getElementById("make").setAttribute('data-target', '#addevent');
                document.getElementById("make").setAttribute('data-toggle', 'modal');
            }
        }
    })
});

function handleNameInput() {
    emptyName.style.display = "none";
    n.style.backgroundColor = "transparent";
}

function handlePhoneInput() {
    emptyPhone.style.display = "none";
    p.style.backgroundColor = "transparent";
}

var toastId = "myToast";

function createToast(text) {
    let toast = `
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
      <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" background-color: white; width:50px; height:20px;">
        <div class="toast-header">
          <strong class="me-auto">Zust</strong>
          <small>Now</small>
          <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
        <div class="toast-body">
          ${text}
        </div>
      </div>
    </div>
  `;
    return toast;
}

function showToast(message) {
    var existingToast = document.getElementById(toastId);
    if (existingToast) {
        existingToast.remove();
    }

    var toastHTML = createToast(message);
    document.body.insertAdjacentHTML("beforeend", toastHTML);
    var toast = document.getElementById(toastId);
    var bsToast = new bootstrap.Toast(toast);
    bsToast.show();
    setTimeout(function () {
        toast.style.display = "none";
    }, 6000);

    var closeButton = toast.querySelector(".btn-close");
    closeButton.addEventListener("click", function () {
        toast.style.display = "none";
    });
}

document.getElementById("okSuccess").addEventListener("click", function () {
    $.ajax({
        url: `/Home/Index`,
        method: "GET",

        success: function () {
            consol.log("OK Working")
        }
    })
});

function DoctorAppointments() {
    $.ajax({
        url: `/Doctor/ShowAllAppointments`,
        method: "GET",

        success: function (data) {
            console.log(data);
        }
    })
}