
var departmentName = "";


var d = document.getElementById("departmentSelect");
var doct = document.getElementById("doctorSelect");
//var n = document.getElementById("name");
var p = document.getElementById("phone");
var message = document.getElementById("message");
var date = document.getElementById("dateSelect");
var time = document.getElementById("exampleFormControlSelect4");


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
                //var id12 = "";
                //for (var k = 0; k < 3; k++) {
                //    id12 += data[i].id[k];
                //}
                content += `
                 <tr onclick="PatientProfile('${data[i].id}')">
                     <td><span class="list-name">${data[i].idv}</span></td> 
                     <td>${data[i].userName}</td>
                     <td>${data[i].phoneNumber}</td>
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
            //console.log("s");
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

function PostFilterAdmin(departmentId) {
    $.ajax({
        url: `/Post/PostFilter?departmentId=${departmentId}`,
        method: "GET",

        success: function (data) {
            var content = "";
            var adminName = "";
            var arrow = "";
            var images = "";

            let contentPatient = "";
            var imagesPatient = "";
            //var categoryList = [];
            var arrowPatient = "";

            var postCategories = "";

            var postCategoriessInCount = 0;

            imagesPatient = "";
            arrowPatient = "";
            //     postCategories += ` 
            //               <li class="align-items-center">
            //   <a onclick="PostFilterAdmin('All')">All</a>
            //   <span>(${data.posts.length})</span>
            //</li>
            //        `;

            //     for (var i = 0; i < data.posts.length; i++) {
            //         postCategoriessInCount = 0;

            //         for (var k = 0; k < data.posts.length; k++) {
            //             if (data.posts[k].department.id === data.posts[i].department.id) {
            //                 postCategoriessInCount += 1;
            //             }
            //         }

            //         if (!categoryList.includes(data.posts[i].department.departmentName)) {
            //             categoryList.push(data.posts[i].department.departmentName);

            //             postCategories += ` 
            //                       <li class="align-items-center">
            //       	     <a onclick="PostFilterAdmin('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
            //       	     <span>(${postCategoriessInCount})</span>
            //       	  </li>
            //                   `;
            //         }
            //     }


            for (var i = 0; i < data.value.posts.length; i++) {
                images = "";
                arrow = "";
                imagesPatient = "";

                for (var k = 0; k < data.value.posts[i].images.length; k++) {
                    if (k == 0) {

                        images += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid"  src="${data.value.posts[i].images[k]}" alt="Responsive image">
                            </div>
                       `;
                    }
                    else {
                        images += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.value.posts[i].images[k]}" alt="Responsive image">
                          </div>
                       `;
                    }
                }

                if (data.value.posts[i].images.length > 1) {
                    arrow += `
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


                content += `

                   <div class="card single_post">
                        <div class="body">
                            <h3 class="m-t-0 m-b-5"><a href="blog-details.html">${data.value.posts[i].title}</a></h3>
                            ${adminName}
                        </div>
                        <div class="body">
                            <div class="img-post m-b-15" style="background-color:rgba(200, 200, 200,0.4);">
                                 <div id="carouselExampleIndicators${data.value.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>
                            <a href="/Post/BlogSingleAdmin?postId=${data.value.posts[i].postId}" target="_self"  title="read more" class="btn btn-round btn-info">Read More</a>
                        </div>
                    </div>
                
                `;



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
            }

            //$(".postFilterCategoriesAdmin").html(postCategories);

            $("#posts").html(content);
        }
    })
}

function PostFilterDoctor(departmentId) {
    $.ajax({
        url: `/Post/PostFilter?departmentId=${departmentId}`,
        method: "GET",

        success: function (data) {
            var content = "";
            var adminName = "";
            var arrow = "";
            var images = "";

            let contentPatient = "";
            var imagesPatient = "";
            //var categoryList = [];
            var arrowPatient = "";

            var postCategories = "";

            var postCategoriessInCount = 0;

            imagesPatient = "";
            arrowPatient = "";
            //     postCategories += ` 
            //               <li class="align-items-center">
            //   <a onclick="PostFilterAdmin('All')">All</a>
            //   <span>(${data.posts.length})</span>
            //</li>
            //        `;

            //     for (var i = 0; i < data.posts.length; i++) {
            //         postCategoriessInCount = 0;

            //         for (var k = 0; k < data.posts.length; k++) {
            //             if (data.posts[k].department.id === data.posts[i].department.id) {
            //                 postCategoriessInCount += 1;
            //             }
            //         }

            //         if (!categoryList.includes(data.posts[i].department.departmentName)) {
            //             categoryList.push(data.posts[i].department.departmentName);

            //             postCategories += ` 
            //                       <li class="align-items-center">
            //       	     <a onclick="PostFilterAdmin('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
            //       	     <span>(${postCategoriessInCount})</span>
            //       	  </li>
            //                   `;
            //         }
            //     }


            for (var i = 0; i < data.value.posts.length; i++) {
                images = "";
                arrow = "";
                imagesPatient = "";

                for (var k = 0; k < data.value.posts[i].images.length; k++) {
                    if (k == 0) {

                        images += `
                             <div class="carousel-item active" style="text-align:center;width:100%;">
                               <img class="img-fluid"  src="${data.value.posts[i].images[k]}" alt="Responsive image" >
                            </div>
                       `;
                    }
                    else {
                        images += `
                          <div class="carousel-item" style="text-align:center;width:100%;">
                               <img class="img-fluid" src="${data.value.posts[i].images[k]}" alt="Responsive image" >
                          </div>
                       `;
                    }
                }

                if (data.value.posts[i].images.length > 1) {
                    arrow += `
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


                content += `

                   <div class="card single_post">
                        <div class="body">
                            <h3 class="m-t-0 m-b-5"><a href="blog-details.html">${data.value.posts[i].title}</a></h3>
                            ${adminName}
                        </div>
                        <div class="body">
                            <div class="img-post m-b-15" style="background-color:rgba(200, 200, 200,0.4);">
                                 <div id="carouselExampleIndicators${data.value.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>
                            <a href="/Post/BlogSingleDoctor?postId=${data.value.posts[i].postId}" target="_self"  title="read more" class="btn btn-round btn-info">Read More</a>
                        </div>
                    </div>
                
                `;



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
            }

            //$(".postFilterCategoriesAdmin").html(postCategories);

            $("#postsDoctor").html(content);
        }
    })
}

function PostFilterPatient(departmentId) {
    $.ajax({
        url: `/Post/PostFilter?departmentId=${departmentId}`,
        method: "GET",

        success: function (data) {
            var contentPatient = "";
            var adminName = "";
            var arrowPatient = "";
            var imagesPatient = "";
            var postCategories = "";
            //var categoryList = [];


            var postCategoriessInCount = 0;

            //console.log(data.value);

            //     postCategories += ` 
            //               <li class="align-items-center">
            //   <a onclick="PostFilterPatient('All')">All</a>
            //   <span>(${data.value.posts.length})</span>
            //</li>
            //        `;

            //     for (var i = 0; i < data.value.posts.length; i++) {
            //         postCategoriessInCount = 0;

            //         for (var k = 0; k < data.value.posts.length; k++) {
            //             if (data.value.posts[k].department.id === data.value.posts[i].department.id) {
            //                 postCategoriessInCount += 1;
            //             }
            //         }

            //         if (!categoryList.includes(data.value.posts[i].department.departmentName)) {
            //             categoryList.push(data.value.posts[i].department.departmentName);

            //             postCategories += ` 
            //                       <li class="align-items-center">
            //       	     <a onclick="PostFilterPatient('${data.value.posts[i].department.id}')">${data.value.posts[i].department.departmentName}</a>
            //       	     <span>(${postCategoriessInCount})</span>
            //       	  </li>
            //                   `;
            //         }
            //     }

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

									<a href="/Post/BlogSingle?postId=${data.value.posts[i].postId}" target="_self" target="_self" class="btn btn-main btn-icon btn-round-full">Read More <i class="icofont-simple-right ml-2  "></i></a>
								</div>
							</div>
						</div>
                `;
            }
            //$(".postFilterCategories").html(postCategories);

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
    //console.log(post);
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
            var contentDoctor = "";
            var adminName = "";
            var arrow = "";
            var images = "";

            let contentPatient = "";
            var imagesPatient = "";
            var arrowPatient = "";

            var categoryList = [];
            var categoryListAdmin = [];
            var categoryListDoctor = [];

            var postCategories = "";
            var postCategoriesAdmin = "";
            var postCategoriesDoctor = "";

            var postCategoriessInCount = 0;
            var postCategoriessInCountAdmin = 0;
            var postCategoriessInCountDoctor = 0;

            imagesPatient = "";
            arrowPatient = "";

            postCategories += ` 
                      <li class="align-items-center">
					     <a style="margin-left:10%" onclick="PostFilterPatient('All')">All</a>
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
				          	     <a style="margin-left:10%" onclick="PostFilterPatient('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCount})</span>
				          	  </li>
                          `;
                }
            }


            postCategoriesAdmin += ` 
                      <li class="align-items-center">
					     <a style="margin-left:10%" onclick="PostFilterAdmin('All')">All</a>
					     <span>(${data.posts.length})</span>
					  </li>
               `;

            for (var i = 0; i < data.posts.length; i++) {
                postCategoriessInCountAdmin = 0;

                for (var k = 0; k < data.posts.length; k++) {
                    if (data.posts[k].department.id === data.posts[i].department.id) {
                        postCategoriessInCountAdmin += 1;
                    }
                }

                if (!categoryListAdmin.includes(data.posts[i].department.departmentName)) {
                    categoryListAdmin.push(data.posts[i].department.departmentName);

                    postCategoriesAdmin += ` 
                              <li class="align-items-center">
				          	     <a style="margin-left:10%" onclick="PostFilterAdmin('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCountAdmin})</span>
				          	  </li>
                          `;
                }
            }



            postCategoriesDoctor += ` 
                      <li class="align-items-center">
					     <a style="margin-left:10%" onclick="PostFilterDoctor('All')">All</a>
					     <span>(${data.posts.length})</span>
					  </li>
               `;

            for (var i = 0; i < data.posts.length; i++) {
                postCategoriessInCountDoctor = 0;

                for (var k = 0; k < data.posts.length; k++) {
                    if (data.posts[k].department.id === data.posts[i].department.id) {
                        postCategoriessInCountDoctor += 1;
                    }
                }

                if (!categoryListDoctor.includes(data.posts[i].department.departmentName)) {
                    categoryListDoctor.push(data.posts[i].department.departmentName);

                    postCategoriesDoctor += ` 
                              <li class="align-items-center">
				          	     <a style="margin-left:10%" onclick="PostFilterDoctor('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCountDoctor})</span>
				          	  </li>
                          `;
                }
            }

            PopularPosts();
            GetAllDoctors();
            GetAllAbouts();

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
                            <div class="img-post m-b-15" style="background-color:rgba(200, 200, 200,0.4);">
                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>
                            <a href="/Post/BlogSingleAdmin?postId=${data.posts[i].postId}" target="_self"  title="read more" class="btn btn-round btn-info">Read More</a>
                        </div>
                    </div>
                
                `;

                contentDoctor += `

                   <div class="card single_post">
                        <div class="body">
                            <h3 class="m-t-0 m-b-5"><a href="blog-details.html">${data.posts[i].title}</a></h3>
                            ${adminName}
                        </div>
                        <div class="body">
                            <div class="img-post m-b-15" style="background-color:black;">
                                 <div id="carouselExampleIndicators${data.posts[i].postId}" class="carousel slide" data-ride="carousel">
                                   <div class="carousel-inner">
                                     ${images}
                                   </div>
                                   ${arrow}
                               </div>
                            </div>
                            <a href="/Post/BlogSingleDoctor?postId=${data.posts[i].postId}" target="_self"  title="read more" class="btn btn-round btn-info">Read More</a>
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

									<a href="/Post/BlogSingle?postId=${data.posts[i].postId}" target="_self" class="btn btn-main btn-icon btn-round-full">Read More <i class="icofont-simple-right ml-2  "></i></a>
								</div>
							</div>
						</div>
                `;
            }

            $("#patientPosts").html(contentPatient);
            $(".postFilterCategories").html(postCategories);
            $(".postFilterCategoriesAdmin").html(postCategoriesAdmin);
            $(".postFilterCategoriesDoctor").html(postCategoriesDoctor);

            $("#postsDoctor").html(contentDoctor);
            $("#posts").html(content);
        }
    })
}

function PopularPosts() {
    $.ajax({
        url: `/Post/PopularPosts`,
        method: "GET",

        success: function (data) {
            var posts = [];
            var content = "";
            var contentDoctor = "";
            var contentAdmin = "";

            for (var i = 0; i < data.popularPostsId.length; i++) {
                for (var k = 0; k < data.posts.length; k++) {
                    if (data.posts[k].postId == data.popularPostsId[i]) {
                        posts.push(data.posts[i]);
                    }
                    //console.log(data.posts[k]);
                }
            }


            for (var i = 0; i < posts.length; i++) {
                content += `
                        <div class="py-2">
            <span class="text-sm text-muted">${posts[i].publishTime}</span>
            <h6 class="my-2"><a href="/Post/BlogSingle?postId=${posts[i].postId}">${posts[i].title}</a></h6>
            	 </div>
                   `;

                contentDoctor += `
                           <li class="row">
								<div class="icon-box col-4">
									<img class="img-fluid img-thumbnail" src="${posts[i].images[0]}" alt="Awesome Image">
								</div>
								<div class="text-box col-8 p-l-0">
									<h5 class="m-b-0"><a href="/Post/BlogSingleDoctor?postId=${posts[i].postId}">${posts[i].title}</a></h5>
									<small class="author-name">By: <a href="/Post/BlogSingleDoctor?postId=${posts[i].postId}">${posts[i].admin.userName}</a></small>
									<small class="date">${posts[i].publishTime}</small>
								</div>
							</li>
                   `;

                contentAdmin += `
                          <li class="row">
								<div class="icon-box col-4">
									<img class="img-fluid img-thumbnail" src="${posts[i].images[0]}" alt="Awesome Image">
								</div>
								<div class="text-box col-8 p-l-0">
									<h5 class="m-b-0"><a href="/Post/BlogSingleAdmin?postId=${posts[i].postId}">${posts[i].title}</a></h5>
									<small class="author-name">By: <a href="/Post/BlogSingleAdmin?postId=${posts[i].postId}">${posts[i].admin.userName}</a></small>
									<small class="date">${posts[i].publishTime}</small>
								</div>
							</li>
                   `;
            }

            $("#popularPosts").html(content);
            $(".popularPostsDoctor").html(contentDoctor);
            $(".popularPostsAdmin").html(contentAdmin);
        }
    })
}

function PatientDoctorFilterPatient(departmentId) {
    $.ajax({
        url: `/DoctorsShow/PatientDoctorFilterPatient?departmentId=${departmentId}`,
        method: "GET",
        success: function (data) {
            //console.log(data);
            var context = "";

            for (var i = 0; i < data.length; i++) {
                context += `

                 <div class="col-lg-3 col-sm-6 col-md-6 mb-4 shuffle-item" data-groups="[&quot;cat1&quot;,&quot;cat2&quot;]">
				    	<div class="position-relative doctor-inner-box">
				    		<div class="doctor-profile">
				    			<div class="doctor-img">
				    				<img src="${data[i].avatar}" alt="doctor-image" class="img-fluid w-100">
				    			</div>
				    		</div>
				    		<div class="content mt-3">
				    			<h4 class="mb-0"><a href="/DoctorsShow/PatientInDoctorProfile?doctorId=${data[i].id}">${data[i].userName}</a></h4>
				    			<p>${data[i].department.departmentName}</p>
				    		</div>
				    	</div>
				    </div>
                
                `;
            }

            $("#patientDoctors").html(context);
        }
    })
}

function checkRadioButton(radioButton) {
    console.log("fsdf");
    if (radioButton.checked) {
        PatientDoctorFilterPatient(radioButton.value);
    }
}

function GetAllDoctors() {
    $.ajax({
        url: `/DoctorsShow/GetAllDoctors`,
        method: "GET",

        success: function (data) {
            var patientDoctors = "";
            var adminDoctors = "";
            var doctorDoctors = "";

            var patientFilter = "";

            for (var i = 0; i < data.departments.length; i++) {
                if (data.departments[i].id == "1") {

                    patientFilter += `
                	<label class="btn active">
                         <input type="radio" name="shuffle-filter" value="${data.departments[i].id}" onchange="checkRadioButton(this)">${data.departments[i].departmentName}</button>
                    </label>
                `;
                    PatientDoctorFilterPatient(data.departments[i].id);
                }
                else {
                    patientFilter += `
                   <label class="btn">
                        <input type="radio" name="shuffle-filter" value="${data.departments[i].id}" onchange="checkRadioButton(this)">${data.departments[i].departmentName}</button>
                   </label>
                `;
                }
            }

            for (var i = 0; i < data.doctors.length; i++) {
                patientDoctors += `
                    <div class="col-lg-3 col-sm-6 col-md-6 mb-4 shuffle-item" data-groups="[&quot;cat1&quot;,&quot;cat2&quot;]">
				    	<div class="position-relative doctor-inner-box">
				    		<div class="doctor-profile">
				    			<div class="doctor-img">
				    				<img style="width:90%; height:22vh; margin:auto;" src="${data.doctors[i].avatar}" alt="doctor-image" class="img-fluid w-100">
				    			</div>
				    		</div>
				    		<div class="content mt-3">
				    			<h4 class="mb-0"><a href="/DoctorsShow/PatientInDoctorProfile('${data.doctors[i].id}')">${data.doctors[i].firstName}<br/>${data.doctors[i].lastName}</a ></h4 >
				    			<p>${data.doctors[i].department.departmentName}</p>
				    		</div>
				    	</div>
				    </div>
                `;


                adminDoctors += `
                     <div class="col-lg-3 col-md-4 col-sm-6">
                         <div class="card xl-blue member-card doctor">
                             <div class="body">
                                 <div class="member-thumb">
                                     <img style="width:90%; height:22vh; margin:auto;" src="${data.doctors[i].avatar}" class="img-fluid" alt="profile-image">
                                 </div>
                                 <div class="detail">
                                     <p class="m-b-0">Dr. ${data.doctors[i].firstName}<br/>${data.doctors[i].lastName}</p>
                                     <p class="text-muted">${data.doctors[i].department.departmentName}</p>
                                     <ul class="social-links list-inline m-t-20">
                                         <li><a title="facebook" href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                         <li><a title="twitter" href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                         <li><a title="instagram" href="#"><i class="zmdi zmdi-instagram"></i></a></li>
                                     </ul>
                                     <a href='/DoctorsShow/AdminInDoctorProfile?doctorId=${data.doctors[i].id}'  class="btn btn-default btn-round btn-simple" >View Profile</a>
                                 </div>
                             </div>
                         </div>
                     </div>
                `;


                doctorDoctors += `
                       <div class="col-lg-3 col-md-4 col-sm-6">
                             <div class="card xl-blue member-card doctor">
                                 <div class="body">
                                     <div class="member-thumb">
                                         <img style="width:90%; height:22vh; margin:auto;" src="${data.doctors[i].avatar}" class="img-fluid" alt="profile-image">
                                     </div>
                                     <div class="detail">
                                         <h4 class="m-b-0">Dr. ${data.doctors[i].firstName}<br/>${data.doctors[i].lastName}</h4>
                                     <p class="text-muted">${data.doctors[i].department.departmentName}</p>
                                         <ul class="social-links list-inline m-t-20">
                                             <li><a title="facebook" href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                             <li><a title="twitter" href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                             <li><a title="instagram" href="#"><i class="zmdi zmdi-instagram"></i></a></li>
                                         </ul>
                                         <a href='/DoctorsShow/DoctorInDoctorProfile?doctorId=${data.doctors[i].id}'  class="btn btn-default btn-round btn-simple" >View Profile</a>
                                     </div>
                                 </div>
                             </div>
                         </div>
                    `;

            }

            $("#adminDoctors").html(adminDoctors);
            $("#doctorDoctors").html(doctorDoctors);
            $("#patientDoctors").html(patientDoctors);
            $("#patientDoctorFilter").html(patientFilter);

        }
    })
}

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
    console.log("GetAllAppointments work");
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
                      <td>${data[i].doctor.firstName} ${data[i].doctor.lastName}</td>
                      <td>${data[i].department.departmentName}</td>
                 </tr>`;
            }
            $("#appointments").html(content);
        }
    })
}


function GetAllAppointmentsOfPatient() {
    console.log("GetAllAppointmentsPatient work");
    $.ajax({
        url: `/Home/ShowAllAppointmentsOfPatient`,
        method: "GET",

        success: function (data) {
            let content = "";

            for (var i = 0; i < data.length; i++) {
                //var doctor = GetAppointmentDoctor(data[i].doctorId)
                content += `
                  <tr>
                      <td>${data[i].id}</td>
                      <td>${data[i].appointmentDate} ${data[i].appointmentTime}</td>
                      <td>${data[i].doctor.firstName} ${data[i].doctor.lastName}</td>
                      <td>${data[i].department.departmentName}</td>
                 </tr>`;
            }
            $("#patientAppointments").html(content);
        }
    })
}


function GetAllDepartment() {
    $.ajax({
        url: `/Departmen/GetAllDepartment`,
        method: "GET",

        success: function (data) {
            let doctorAndAdminDepartments = "";
            let patientDe = "";


            for (var i = 0; i < data.departments.length; i++) {

                doctorAndAdminDepartments += `
                    <div class="col-lg-4 col-md-6 col-sm-12">
                          <div class="card project_widget">
                              <div class="pw_img" style="text-align:center; padding:7%;">
                                  <img class="img-fluid" src="${data.departments[i].imageUrl}" alt="About the image">
                              </div>
                              <div class="pw_content">
                                  <div class="pw_header" style="text-align:center;">
                                      <h3 style="margin:-5%">${data.departments[i].departmentName}</h3>
                                  </div>
                                  <div class="pw_meta">
                                      <p>${data.departments[i].content}</p>
                                  </div>
                              </div>
                          </div>
                      </div>
                    `;

                patientDe += `
			        	<div class="col-lg-4 col-md-6 " style="width:30%; heught:30vh;">
			        		<div class="department-block mb-5">			<img style="width:90%; height:35vh; margin-top:5%" src="${data.departments[i].imageUrl}" alt="" class="img-fluid w-100">
			        			<div class="content" style="overflow: clip;">
			        				<h4 class="mt-4 mb-2 title-color">${data.departments[i].departmentName}</h4>
			        				<p class="mb-4">${data.departments[i].content}</p>
			        				<a href="/Departmen/DepartmentSinglePatient?departmentId=${data.departments[i].id}" class="read-more">Learn More  <i class="icofont-simple-right ml-2"></i></a>
			        			</div>
			        		</div>
			        	</div>
                    `;

            }

            $("#departmentsAdmin").html(doctorAndAdminDepartments);
            $("#doctorDepartments").html(doctorAndAdminDepartments);
            $("#patientDepartments").html(patientDe);
        }
    })
}

//function GetAllDepartment() {
//    $.ajax({
//        url: `/Department/GetAllDe`,
//        method: "GET",

//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                DoctorCall(data[i].id);
//            }
//        }
//    })
//}

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
    //if (availableDoctor != null) {
    $.ajax({
        url: `/Home/GetAvailableDays?doctorId=${availableDoctor}`,
        method: "GET",
        success: function (data) {
            console.log(data);

            var content = `<option value="" selected disabled hidden>Select a date</option>`

            for (var i = 0; i < data.length; i++) {
                content += `
                     <option value="${data[i]}">${data[i]}</option>`;
            }

            if (data.length > 0) {
                date.style.backgroundColor = "transparent";
            }
            else {
                date.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
            }

            $("#dateSelect").html(content);
        }
    });
}

function GetTime() {
    var availableDoctor = $("#doctorSelect").val();
    var dateValue = document.getElementById("dateSelect").value;

    console.log(dateValue);

    $.ajax({
        url: `/Home/GetAvailableTimes?doctorId=${availableDoctor}&appointmentDate=${dateValue}`,
        method: "GET",
        success: function (data) {
            console.log(data)
            var content = `<option value="" selected disabled hidden>Select a time</option>`;

            for (var i = 0; i < data.length; i++) {
                content += `
                     <option value="${data[i]}">${data[i]}</option>`;
            }


            if (data.length > 0) {
                time.style.backgroundColor = "transparent";
            }
            else {
                time.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
            }

            $("#exampleFormControlSelect4").html(content);
        }
    });
}

function SendSMS() {
    var phoneNumber = $("#phone").val();
    console.log("send sms called");
    $.ajax({
        url: `/SendSMS/SendText`,
        method: 'POST',
        success: function (data) {
            console.log('Message sent:', data.messageSid);
            DoctorAppointments();
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    })
}


function SendEmail() {
    console.log("send email called");
    $.ajax({
        url: `/SendEmail/SendEmailText?`,
        method: "POST",

        success: function () {
            console.log("Email sent");
            DoctorAppointments();
        },
        error: function (xhr, status, error) {
            console.log("Error : ", error);
        }
    })
}


document.getElementById("departmentSelect").addEventListener("change", function () {
    var departmentId = this.value;

    var doctorSelect = document.getElementById("doctorSelect");
    doctorSelect.innerHTML = "";

    d.style.backgroundColor = "transparent";


    $.ajax({
        url: `/Home/getDoctors?departmentId=${departmentId}`,
        method: "GET",



        success: function (data) {
            var content = "";
            //console.log(data);
            var option = document.createElement("option");
            option.text = "Select a doctor";
            option.value = 0;
            //option.disabled = true;
            doctorSelect.appendChild(option);
            data.forEach(function (doctor) {
                var option = document.createElement("option");
                option.text = doctor.firstName + " " + doctor.lastName;
                option.value = doctor.id;
                doctorSelect.appendChild(option);
            });
            //GetDay();
            //GetTime();

            if (data.length > 0) {
                doct.style.backgroundColor = "transparent";
                date.style.backgroundColor = "transparent";
                time.style.backgroundColor = "transparent";

            }
            else {
                document.getElementById("dateSelect").innerHTML = "";
                document.getElementById("exampleFormControlSelect4").innerHTML = "";
                doct.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
                date.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
                time.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
            }
        }
    })
});

//function AddBigAboutFirst() {
//    var title = document.getElementById("firstBigTitle");
//    var content = document.getElementById("firstBigContent");
//    //console.log(title);
//    //if (title.value.trim() != "" && content.value.trim() != "") {
//    //console.log("suc");
//    $.ajax({
//        url: `/Admin/AddAbout`,
//        method: "POST",
//        data: { BigTitle: title.value, FirstContent: content.value },
//        dataType: "json",

//        success: function (data) {
//            console.log("aynthing is null");
//        }
//    })
//    //}
//    //else {
//    if (title.value.trim() == "") {
//        title.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//    }
//    if (content.value.trim() == "") {
//        content.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//    }
////}
//}

//function HandleBigTitleChange() {
//    //console.log("dxcxcx");
//    var title = document.getElementById("firstBigTitle");
//    if (title.value.trim() == "") {
//        title.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//    }
//    else {
//        title.style.backgroundColor = "transparent";
//    }
//}

//function HandleBigContentChange() {
//    //console.log("df");
//    var content = document.getElementById("firstBigContent");
//    if (content.value.trim() == "") {
//        content.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//    }
//    else {
//        content.style.backgroundColor = "transparent";
//    }
//}

function GetAllAbouts() {
    $.ajax({
        url: `/About/GetAllAboutsUsers`,
        method: "GET",

        success: function (data) {

            var patientContent = "";
            var patientAboutPageFirstContent = "";
            var bigTitle = "";
            var patientInDoctors = "";

            var aboutInDoctorsAndAdmin = "";

            for (var i = 0; i < data.doctors.length; i++) {
                patientInDoctors += `
               <div class="col-lg-3 col-md-6 col-sm-6">
				 	<div class="team-block mb-5 mb-lg-0">
				 		<img src="${data.doctors[i].imageUrl}" alt="" class="img-fluid w-100">
               
				 		<div class="content">
				 			<h4 class="mt-4 mb-0"><a href="doctor-single.html">${data.doctors[i].firstName}  ${data.doctors[i].lastName}</a></h4>
				 		</div>
				 	</div>
				 </div>
                `;
            }

            for (var i = 0; i < data.abouts.length; i++) {
                if (data.abouts[i].title != null) {

                    aboutInDoctorsAndAdmin += `
                     <div class="col-lg-4 col-md-6 col-sm-12">
                            <div class="card project_widget">
                                <div class="pw_img">
                                    <img class="img-fluid" src="${data.abouts[i].imageUrl}" alt="About the image">
                                </div>
                                <div class="pw_content">
                                    <div class="pw_header">
                                        <h6>${data.abouts[i].title}</h6>
                                    </div>
                                    <div class="pw_meta">
                                        <p>${data.abouts[i].content}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                     `;

                    patientContent += `
                        <div class="col-lg-3 col-md-6">
				        	<div class="about-block-item mb-5 mb-lg-0">
				        		<img src="${data.abouts[i].imageUrl}" alt="" class="img-fluid w-100">
				        		<h4 class="mt-3">${data.abouts[i].title}</h4>
				        		<p>${data.abouts[i].content}</p>
				        	</div>
				        </div>
                        `;
                }
                else {
                    bigTitle += `
					   <h2 class="title-color">${data.abouts[i].bigTitle}</h2>
                    `;

                    patientAboutPageFirstContent += `
                
                       <p>${data.abouts[i].firstContent}</p>
                
                     `;
                }
            }

            $("#patientAbouts").html(patientContent);
            $("#bigTitle").html(bigTitle);
            $("#aboutsAdmin").html(aboutInDoctorsAndAdmin);
            $("#doctorsAdmin").html(aboutInDoctorsAndAdmin);
            //$("#aboutsAdmin").html(aboutInDoctorsAndAdmin);
            $("#patientAboutPageFirstContent").html(patientAboutPageFirstContent);
        }
    })
}
document.getElementById("doctorSelect").addEventListener("change", function () {
    GetDay();
    //GetTime();
});

document.getElementById("dateSelect").addEventListener("change", function () {
    GetTime();
});


//var btn = $("#make");
//var emptyPhone = document.getElementById("emptyPhone");
//var emptyName = document.getElementById("emptyName");


function CallSuccessPage() {
    $.ajax({
        url: `/Home/SuccessPay`,
        method: "GET",

        //success: function () {
        //    console.log("Send page called");
        //}
    })
}

function CallAppointment() {
    //var department = $("departmentSelect").val();
    //var doctor = $("doctorSelect").val();
    //var fullName = $("#name").val();
    //var phone = $("#phone").val();
    //var date1 = $("exampleFormControlSelect3").val();
    //var time1 = $("exampleFormControlSelect4").val();
    //var message1 = $("message").val();

    console.log(d.value);          //any
    console.log(doct.value);       //none
    console.log(p.value);          //0
    console.log(message.value);    //any
    console.log(date.value);       //none
    console.log(time.value);       //none
    if (d.value != null && doct.value != " " && p.value != "0"
        && message.value != " " && date.value != " " && time.value != " ") {
        console.log("intro");
        var s = document.getElementById("success");
        s.innerHTML = `<div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <button type="submit" onclick="SendSMS()" class="btn btn-primary btn-round waves-effect" style="background-color:rgba(34,58,102,255); border-radius:30px; color:white; margin:20%; width:35%; margin-left:32%;">Send SMS</button>
                                    <button type="submit" onclick="SendEmail()" class="btn btn-primary btn-round waves-effect" style="background-color:rgba(34,58,102,255); border-radius:30px; color:white; margin-left:32%; margin-bottom:20%; width:35%">Send Email</button>
                                </div>
                            </div>`;
        $.ajax({
            url: `/Home/Appointment`,
            method: "POST",
            data: { DoctorId: doct.value, DepartmentId: d.value, PhoneNumber: p.value, Message: message.value, appointmentDate: date.value, appointmentTime: time.value },
            dataType: "json",

            success: function () {
                console.log("aynthing is null");
                //CallSuccessPage();

            }
        })
    }

    if (d.value.trim() == "") {
        console.log("department is null");
        d.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        d.style.borderColor = "transparent";
    }

    if (doct.value.trim() == "Select a doctor") {
        console.log("doctor is null");
        doct.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        doct.style.borderColor = "transparent";
    }

    if (p.value == "0" || phone.length < 9) {
        console.log("phone is null");
        p.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        p.style.borderColor = "transparent";
    }

    //console.log(date.value);
    if (date.value == "Select a date") {
        console.log("date is null");
        date.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        date.style.borderColor = "transparent";
    }

    if (time == null || time.value == "" || time.value == "Select a time") {
        console.log("time is null");
        time.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        time.style.borderColor = "transparent";
    }

    //console.log(message);
    if (message.value.trim() == "") {
        console.log("message is null");
        message.style.borderColor = "rgba(255, 99, 71, 0.8)";
        //emptyName.style.display = "inline-block";
    }
    else {
        message.style.borderColor = "transparent";
    }
}

function ChangePhone() {
    if (p.innerHTML != "") {
        p.style.backgroundColor = "transparent";
    }
    else {
        p.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
    }

}

function ChangeDate() {
    date.style.backgroundColor = "transparent";
}

function ChangeTime() {
    time.style.backgroundColor = "transparent";
}

function ChangeMessage() {
    if (message.value.trim != "") {
        message.style.backgroundColor = "transparent";
    }
    else {
        message.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
    }
}

//document.getElementById("make").addEventListener("click", function () {
//    $.ajax({
//        url: `/Home/CheckInputs?phoneNumber=${p.value}&fullName=${n.value}&date=${date.value}&time=${time.value}&message=${message.value}`,
//        method: "GET",
//        success: function (data) {

//            if (data.includes("fullname is null")) {
//                n.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//                emptyName.style.display = "inline-block";
//            }
//            if (data.includes("phone is null")) {
//                p.style.backgroundColor = "rgba(255, 99, 71, 0.8)";
//                emptyPhone.style.display = "inline-block";
//                console.log("phone is null");
//            }
//            if (data.includes("okay")) {

//            }

//            if (data === "") {
//                document.getElementById("make").setAttribute('data-target', '#addevent');
//                document.getElementById("make").setAttribute('data-toggle', 'modal');
//            }
//        }
//    })
//});

//function handleNameInput() {
//    emptyName.style.display = "none";
//    n.style.backgroundColor = "transparent";
//}

function handlePhoneInput() {
    emptyPhone.style.display = "none";
    p.style.backgroundColor = "transparent";
}

//var toastId = "myToast";

//function createToast(text) {
//    let toast = `
//    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
//      <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" background-color: white; width:50px; height:20px;">
//        <div class="toast-header">
//          <strong class="me-auto">Zust</strong>
//          <small>Now</small>
//          <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
//        </div>
//        <div class="toast-body">
//          ${text}
//        </div>
//      </div>
//    </div>
//  `;
//    return toast;
//}

//function showToast(message) {
//    var existingToast = document.getElementById(toastId);
//    if (existingToast) {
//        existingToast.remove();
//    }

//    var toastHTML = createToast(message);
//    document.body.insertAdjacentHTML("beforeend", toastHTML);
//    var toast = document.getElementById(toastId);
//    var bsToast = new bootstrap.Toast(toast);
//    bsToast.show();
//    setTimeout(function () {
//        toast.style.display = "none";
//    }, 6000);

//    var closeButton = toast.querySelector(".btn-close");
//    closeButton.addEventListener("click", function () {
//        toast.style.display = "none";
//    });
//}

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
            let content = "";

            for (var i = 0; i < data.length; i++) {
                //var doctor = GetAppointmentDoctor(da ta[i].doctorId)
                content += `
                  <tr>
                      <td>${data[i].id}</td>
                      <td>${data[i].appointmentDate} ${data[i].appointmentTime}</td>
                      <td>${data[i].patient.firstName} ${data[i].patient.lastName}</td>
                 </tr>`;
            }
            $("#doctorAppointments").html(content);
        }
    })
}

document.getElementById("doctorSelect").addEventListener("change", function () {
    GetDay();
    GetTime();
    SendSMS();
});

function handleRoomId() {
    var time = document.getElementById("scheduleSelect").value;
    console.log("Df");
    var roomSelect = document.getElementById("roomSelect");
    $.ajax({
        url: `/Admin/FilterRooms?time=${time}`,
        method: "GET",

        success: function (data) {
            let content = "";
            content += '<option value="" selected disabled hidden></option>';
            for (var i = 0; i < data.length; i++) {
                content += `<option value="${data[i].id}">${data[i].roomNo}</option>`
            }
            $("#roomSelect").html(content);
            //data.forEach(function (room) {
            //    var option = document.createElement("option");
            //    option.text = room.roomNo;
            //    option.value = room.id;
            //    roomSelect.appendChild(option);
            //});
        }
    })
}


//function DoctorPatients() {
//    $.ajax({
//        url: `/Doctor/GetPatientsOfDoctor`,
//        method: "GET",

//        success: function (data) {
//            let content = ``;
//            for (var i = 0; i < data.length; i++) {
//                content += `${data[i].userName}`;

//            }
//        }

//    })
//}