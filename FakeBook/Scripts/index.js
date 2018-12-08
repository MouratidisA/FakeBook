
$(document).ready(function () {

    // Random RGB Function 
    function random_rgba() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }

    //fill mention drop down
    const dropdown = $('#mentionFriend');

    function mentions(dropdown) {
        $.ajax({
            url: `/api/Users`,
            type: 'GET',
            success: function (data) {
                dropdown.empty();
                dropdown.append('<option value selected>Mention a Friend</option>');
                $.each(data,
                    function (key, value) {
                        dropdown.append('<option value=' + value.Id + '>' + value.LastName + " " + value.FirstName + '</option>');
                         
                    });
            },
            error: function (errorThrown) {
                alert(errorThrown);
            }
        });
    };

    mentions(dropdown);

    /*
     * Click Event Listeners
     */

    $('#postBtn').click(function () {
        $('#newPostFrom').validate({
            debug: true,
            ignore: '*:not([name])',
            highlight: function (element) {
                $(element).fadeOut(function () {
                    $(element).fadeIn();
                });
            },

            errorClass: "text-danger error",
            errorElement: 'span',
            rules: {
                Text: "required"
            },
            messages: {
                Text: "Text is required!"
            },
            submitHandler: function () {
                $.ajax({
                    url: `/api/Posts`,
                    type: 'POST',
                    data: $('#newPostFrom').serialize(),
                    success: function () {
                        alert("New Post success!");
                        location.reload();
                        //TODO reload only post on success
                    },
                    error: function (errorThrown) {
                        alert("New Post failed!");
                    }
                });

            }
        });
    });

    $('body').on('click', '.btn-edit', function () {
        var ownerId = sessionStorage.getItem("userId");
        var postId = this.id;
        $.ajax({
            url: `../api/Posts/${ownerId}/${postId}`,
            type: 'GET',
            success: function (data) {
                const modal = $('#editModal');
                modal.modal('show');
                //TODO remove this alert before Release 
                alert('modal values: \n ' + 'RandomColor: ' + data.RandomColor + '\n ID:' + data.ID + '\n Bold:' + data.Bold + '\n OwnerId:' + data.OwnerId + '\n Text:' + data.Text + '\n FriendId:' + data.FriendId);
                //set text BOLD or not 
                if (data.Bold) {
                    modal.find('.Text').css('font-weight', 'bold');
                }
                modal.find('#updBold').attr("checked", data.Bold);
                //set Background color or not 
                var hasColor = data.RandomColor != "none" ? true : false;
                if (hasColor) {
                    modal.find('.Text').css('background-color', data.RandomColor.toString());

                    if (sessionStorage.getItem("Role") != "Admin") {
                        modal.find('.Text').attr('readonly', true);
                    }
                    
                    modal.find('#updRandomColorValue').attr('value', data.RandomColor.toString());
                    modal.find('#updRandomColor').attr("checked", true);
                } else {
                    modal.find('#updRandomColor').attr("checked", false);
                    modal.find('#updRandomColorValue').attr('value', 'none');
                }
                modal.find('.OwnerId').val(data.OwnerId); 
                //loads friend list
                var dropdown = $('.FriendId');
                mentions(dropdown);
                // default selected item in dropdown list
                //TODO Mention Friend on default selection   
                 
                $(".FriendId option[value='" + data.FriendId + "']").attr('selected', true);

                modal.find('.ID').val(data.ID);
                modal.find('.Text').val(data.Text);

            },
            error: function (errorThrown) {
                console.log(errorThrown);
            }
        });


    });

    $('body').on('click', '.update-btn', function () {
        const modal = $('#editModal');
        var postId = modal.find('.ID').val();
        $('#updFrm').validate({
            ignore: '*:not([name])',
            debug: true,
            highlight: function (element) {
                $(element).fadeOut(function () {
                    $(element).fadeIn();
                });
            },
            errorClass: "text-danger error",
            errorElement: 'span',
            rules: {
                Text: "required"
            },
            messages: {
                Text: "Text is required!"
            },
            submitHandler: function () {
                $.ajax({
                    url: `../api/Posts/upd/${postId}`,
                    type: 'PUT',
                    data: $('#updFrm').serialize(),
                    success: function () {
                        modal.trigger('click.dismiss.bs.modal');
                        //TODO update only div from DOM
                        location.reload();
                    },
                    error: function (errorThrown) {
                        console.log(errorThrown);
                        alert("fAILED UPDATE");
                    }
                });
            }
        });

    });

    $('body').on('click', '.delete', function () {
        var postId = this.id;
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                $.ajax({
                    url: `../api/Posts/delete/${postId}`,
                    type: 'DELETE',
                    data: $('#updFrm').serialize(),
                    success: function () {
                        //TODO remove from DOM
                        location.reload();
                    },
                    error: function (errorThrown) {
                        console.log(errorThrown);
                        alert("Delete Failed");
                    }
                });
            }

        });
    });

    $('#RandomColor').click(function () {
        if ($(this).is(':checked')) {
            $('#RandomColorValue').attr('value', random_rgba());
        } else {
            $('#RandomColorValue').attr('value', "none");
        }
    });

    $('#Bold').click(function () {
        if ($(this).is(':checked')) {
            $('#BoldValue').attr('value', true);
        } else {
            $('#BoldValue').attr('value', false);
        }
    });

    $('#updRandomColor').click(function () {
        if ($(this).is(':checked')) {
            $('#updRandomColorValue').attr('value', random_rgba());
        } else {
            $('#updRandomColorValue').attr('value', "none");
        }
    });

    $('#updBold').click(function () {
        if ($(this).is(':checked')) {
            $('#updBoldValue').attr('value', true);
        } else {
            $('#updBoldValue').attr('value', false);
        }
    });


    /**
     * Wall list for every user's posts     
     * using userId information for user's post relational key , if userId get values null or empty then user is not logged in 
     */
    if (sessionStorage.getItem("userId") != 'null' && sessionStorage.getItem("userId") != '') {
        wallListFill(sessionStorage.getItem("userId"));
    }

    function wallListFill(ownerId) {
        if (sessionStorage.getItem('Role') == 'Admin') {
            $.ajax({
                url: `/api/Posts`,
                type: 'GET',
                success: function (postList) {
                    //   alert("Post load success! ADMIN");
                    postList.forEach(function (post) {

                        var post_item =
                            "<div class='container  Post-item'> " +
                            "<textarea readonly style='margin-top: 5px; width: 100%; background-color: " + post.RandomColor + ";'>" +
                            post.Text +
                            "</textarea>" +
                            "<label class='label-friend' name='FriendId'> " + post.FriendId + "</label>  " +
                            "<Button id= '" + post.ID + "' class='btn btn-sm wall-btn btn-edit'> <i class='fa fa-pencil-square-o post-edit-icon ' aria-hidden='true'></i> Edit</Button> " +
                            "<Button id= '" + post.ID + "'  class='btn btn-sm btn-danger delete'><i class='fa fa-trash-o' aria-hidden='true'></i> Delete</Button></div>";


                        $('#PostList').prepend(post_item);
                    });

                },
                error: function (errorThrown) {
                    alert("Post load failed!");
                }
            });
        } else {
            $.ajax({
                url: `/api/Posts/own/${ownerId}`,
                type: 'GET',
                success: function (postList) {
                    //        alert("Post load success Basic User!");
                    postList.forEach(function (post) {

                        var post_item =
                            "<div class='container  Post-item'> " +
                            "<textarea readonly style='margin-top: 5px; width: 100%; background-color: " + post.RandomColor + ";'>" +
                            post.Text +
                            "</textarea>" +
                            "<label class='label-friend' name='FriendId'> " + post.FriendId + "</label>  " +
                            "<Button id= '" + post.ID + "' class='btn btn-sm wall-btn btn-edit'> <i class='fa fa-pencil-square-o post-edit-icon ' aria-hidden='true'></i> Edit</Button> </div>";

                        $('#PostList').prepend(post_item);
                    });

                },
                error: function (errorThrown) {
                    alert("Post load failed!");
                }
            });
        }

    }
});