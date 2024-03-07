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
					     <a onclick="PostFilterPatient('All')">All</a>
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
				          	     <a onclick="PostFilterPatient('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCount})</span>
				          	  </li>
                          `;
                }
            }


            postCategoriesAdmin += ` 
                      <li class="align-items-center">
					     <a onclick="PostFilterAdmin('All')">All</a>
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
				          	     <a onclick="PostFilterAdmin('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCountAdmin})</span>
				          	  </li>
                          `;
                }
            }



            postCategoriesDoctor += ` 
                      <li class="align-items-center">
					     <a onclick="PostFilterDoctor('All')">All</a>
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
				          	     <a onclick="PostFilterDoctor('${data.posts[i].department.id}')">${data.posts[i].department.departmentName}</a>
				          	     <span>(${postCategoriessInCountDoctor})</span>
				          	  </li>
                          `;
                }
            }

            PopularPosts();
            GetAllDoctors();

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
				    			<p>Cardiology</p>
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
        //PatientDoctorFilterPatient(radioButton.value);
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
                         <input type="radio" name="shuffle-filter" value="${data.departments[i].id}" onclick="checkRadioButton()"/>${data.departments[i].departmentName}
                    </label>
                `;
                    PatientDoctorFilterPatient(data.departments[i].id);
                }
                else {
                    patientFilter += `
                   <label class="btn">
                       <input type="radio" id="shuffle-filter" name="shuffle-filter" value="${data.departments[i].id}" onclick="checkRadioButton()"/>${data.departments[i].departmentName}
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
				    				<img src="${data.doctors[i].avatar}" alt="doctor-image" class="img-fluid w-100">
				    			</div>
				    		</div>
				    		<div class="content mt-3">
				    			<h4 class="mb-0"><a href="/DoctorsShow/PatientInDoctorProfile('${data.doctors[i].id}')">${data.doctors[i].userName}</a></h4>
				    			<p>Cardiology</p>
				    		</div>
				    	</div>
				    </div>
                `;


                adminDoctors += `
                     <div class="col-lg-3 col-md-4 col-sm-6">
                         <div class="card xl-blue member-card doctor">
                             <div class="body">
                                 <div class="member-thumb">
                                     <img src="${data.doctors[i].avatar}" class="img-fluid" alt="profile-image">
                                 </div>
                                 <div class="detail">
                                     <h4 class="m-b-0">Dr. ${data.doctors[i].userName}</h4>
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
                `


                doctorDoctors += `
                       <div class="col-lg-3 col-md-4 col-sm-6">
                             <div class="card xl-blue member-card doctor">
                                 <div class="body">
                                     <div class="member-thumb">
                                         <img src="${data.doctors[i].avatar}" class="img-fluid" alt="profile-image">
                                     </div>
                                     <div class="detail">
                                         <h4 class="m-b-0">Dr. ${data.doctors[i].userName}</h4>
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
        url: `/Departmen/GetAllDepartment`,
        method: "GET",

        success: function (data) {
            let doctorAndAdminDepartments = "";
            let patientDe = "";


            for (var i = 0; i < data.departments.length; i++) {
                if (data.departments[i].id != "1") {

                    doctorAndAdminDepartments += `
                    <div class="col-lg-4 col-md-6 col-sm-12">
                          <div class="card project_widget">
                              <div class="pw_img">
                                  <img class="img-fluid" src="${data.departments[i].imageUrl}" alt="About the image">
                              </div>
                              <div class="pw_content">
                                  <div class="pw_header">
                                      <h6>${data.departments[i].departmentName}</h6>
                                  </div>
                                  <div class="pw_meta">
                                      <p>${data.departments[i].content}</p>
                                  </div>
                              </div>
                          </div>
                      </div>
                    `;

                    patientDe += `
			        	<div class="col-lg-4 col-md-6 ">
			        		<div class="department-block mb-5">			<img src="${data.departments[i].imageUrl}" alt="" class="img-fluid w-100">
			        			<div class="content">
			        				<h4 class="mt-4 mb-2 title-color">${data.departments[i].departmentName}</h4>
			        				<p class="mb-4">${data.departments[i].content}</p>
			        				<a href="/Departmen/DepartmentSinglePatient?departmentId=${data.departments[i].id}" class="read-more">Learn More  <i class="icofont-simple-right ml-2"></i></a>
			        			</div>
			        		</div>
			        	</div>
                    `;
                }
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

//function GetAllDoctors() {
//    //console.log("doctor");
//    $.ajax({
//        url: `/Admin/GetAllDoctors`,
//        method: "GET",

//        success: function (data) {
//            let content = "";

//            for (var i = 0; i < data.length; i++) {
//                //var doctor = GetAppointmentDoctor(data[i].doctorId)
//                var department = "";
//                //GetDoctorIdDepartment(data[i].id);
//                content += `
//                            <div class="col-lg-3 col-md-4 col-sm-6">
//                                <div class="card xl-blue member-card doctor">
//                                    <div class="body">
//                                        <div class="member-thumb">
//                                            <img src="/AccessaryFiles/images/${data[i].avatar}" class="img-fluid" alt="profile-image">
//                                        </div>
//                                        <div class="detail">
//                                            <h4 class="m-b-0">Dr. ${data[i].userName}</h4>
//                                            <ul class="social-links list-inline m-t-20">
//                                                <li><a title="facebook" href="#"><i class="zmdi zmdi-facebook"></i></a></li>
//                                                <li><a title="twitter" href="#"><i class="zmdi zmdi-twitter"></i></a></li>
//                                                <li><a title="instagram" href="#"><i class="zmdi zmdi-instagram"></i></a></li>
//                                            </ul>
//                                            <a href='/Admin/DoctorProfile?doctorId=${data[i].id}'  class="btn btn-default btn-round btn-simple" >View Profile</a>
//                                        </div>
//                                    </div>
//                                </div>
//                            </div>
//                            `
//            }
//            $("#doctors").html(content);
//        }
//    })
//}

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

function SendSMS() {
    $.ajax({
        url: `/SendSMS/SendText`,
        method: "POST",
        success: function (data) {
            console.log(data);
        }
    })
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
    SendSMS();
});