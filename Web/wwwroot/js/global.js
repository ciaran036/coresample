$(function () {

    $(document).ajaxError(function (e, xhr) {
        if (xhr.status === 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location = response.LogOnUrl;
        }
    });

    // get visibility state from local storage 
    var miniNavbar = localStorage.getItem('mini-navbar');
    // 
    if (miniNavbar == 'true') {
        // console.log('miniNavbar: ' + miniNavbar);
        $("body").toggleClass("mini-navbar");
        SmoothlyMenu();
    }

    if ($(window).width() < 767) {
        $(".nav-tabs li a .text-container").addClass("hidden");
    }

    if ($(window).width() < 767) {
        $(".nav-tabs li a .text-container").addClass("hidden");
    }

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $('ul.dropdown-menu li').each(function () {

        //if its not the divider and has no text then remove the li
        if ($(this).text() == "" && !$(this).hasClass("divider")) {
            $(this).remove();
        }

        //if its the last li and is the divider li then remove it
        if ($(this).is(":last-child") && $(this).hasClass("divider")) {
            $(this).remove();
        }

        //if divider is first child then remove it
        if ($(this).is(":first-child") && $(this).hasClass("divider")) {
            $(this).remove();
        }
    });

    $('ul.dropdown-menu').each(function () {
        if ($(this).has("li").length == 0) {
            $(this).prev("button").remove();
        }
    });

    $('input[type=text], input[type=password], textarea, input[type=email]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0) {
                label.append('<span style="color:#676a6c"> *</span>');
            }
        }
    });

    $('select').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0) {
                label.append('<span style="color:#676a6c"> *</span>');
            }
        }
    });

    styleCheckboxes();
    enableBootstrapTooltips();

    $(".onoffswitch-checkbox").on("change", function () {

        if ($(this).attr("checked")) {
            //stop deputising
            setTimeout(function () { window.location.href = "/Shared/StopDeputising"; }, 500);
        }
        else {
            $(".onoffswitch-checkbox").not(this).attr("checked", false);

            var id = $(this).data("userid");
            $("#SelectedDeputyId").val(id);
            setTimeout(function () { $(".deputising-form").submit(); }, 500);
        }
    });

    try {
        // fix for chrome that won't validate dd/MM/yyyy formats
        jQuery.validator.methods.date = function (value, element) {
            var dateRegex = /^(0?[1-9]\/|[12]\d\/|3[01]\/){2}(19|20)\d\d$/;
            return this.optional(element) || dateRegex.test(value);
        };
    }
    catch (ex) { }

    initializeTooltipValidation();
    initialiseClockPicker();
    renderToastNotification();

    /* Entity Delete / Deactivation */

    $('.delete-alert').click(function (e) {

        e.preventDefault();
        var entity = $(this).data("entity");
        var href = $(this).attr("href");
        deleteAlert(false, entity, href);
    });

    $('.delete-alert').css('color', 'crimson');

    $(".permanent-delete-alert").click(function (e) {
        e.preventDefault();
        var entity = $(this).data("entity");
        var href = $(this).attr("href");
        deleteAlert(true, entity, href);
    });

    function deleteAlert(permanentDeletion, entity, href) {

        var text = permanentDeletion
            ? "This will permanently erase this " + entity.toLowerCase() + "."
            : "Only Admin users will be able to reactivate this " + entity.toLowerCase() + ".";

        var confirmationText = permanentDeletion ? entity + " has been permanently deleted" : entity + " has been deactivated";
        var confirmationTitle = permanentDeletion ? "Deleted!" : "Deactivated";
        var confirmButtonText = permanentDeletion ? "delete" : "deactivate";

        swal({
            title: "Are you sure?",
            text: text,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, " + confirmButtonText + " it",
            closeOnConfirm: false,
            closeOnCancel: false
        }).then(function (result) {
            if (result.dismiss) {
                swal("Cancelled", entity + " has not be deactivated.", "error");
            } else {
                $.ajax({
                    type: "GET",
                    url: href,
                    success: function (data) {
                        swal({
                            title: confirmationTitle,
                            text: confirmationText,
                            type: "success"
                        }).then(function () {
                            window.location.href = data;
                        });
                    },
                    error: function (data) {
                        swal("Error", "Something went wrong there. Please contact the systems administrator.", "error");
                    }
                });
            }

        });
    }

    $('.logoff').click(function (e) {

        e.preventDefault();
        swal({
            title: "Are you sure?",
            text: "This will log you out of the system",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, log off",
        }).then(function (result) {
            if (result.dismiss) {
                swal("Cancelled", "You have not been logged out of the system", "error");
            } else {
                $.ajax({
                    type: "GET",
                    url: "/Account/LogOff",
                    success: function (data) {
                        swal({
                            title: "Logged Off!",
                            text: "You have logged out.",
                            type: "success"
                        }).then(function () { window.location.href = "/Account/Login"; });
                    },
                    error: function (data) {
                        swal("Error", "Something went wrong there. Please contact the systems administrator", "error");
                    }
                });
            }
        });
    });

    $("table").each(function () {
        var pageSize = $(this).data("pagesize") ? $(this).data("pagesize") : 15;
        var createFunc = $(this).data("create");
        if ($("tbody tr", $(this)).length < pageSize) {
            //Number of missing rows
            var missing = pageSize - $("tbody tr", $(this)).length;
            //String builder
            var toAppend;
            //Number of columns
            var columns = $("thead tr:first-child th", $(this)).length;
            for (var x = 0; x < missing; x++) {
                if (createFunc != null) {
                    toAppend += "<tr data-url=" + createFunc + ">";
                }
                else {
                    toAppend += "<tr>";
                }
                for (var y = 0; y < columns; y++) {
                    toAppend += "<td>&nbsp;</td>";
                }
                toAppend += "</tr>";
            }
            $("tbody", $(this)).append(toAppend);
            // rebind click
            $("table tbody tr").click(function () {
                if ($(this).data("url")) {
                    window.location.href = $(this).data("url");
                }
            });
        }
    });

    $("table tbody tr").click(function () {
        if ($(this).data("url")) {
            window.location.href = $(this).data("url");
        }
    });

    advancedSearch();

    $("textarea").each(function () {
        var charLimitInitial = $(this).data("val-maxlength-max");
        if (charLimitInitial) {
            var charLengthInitial = $(this).val().length;
            $(this).parent(".input-group")
                .after("<span class='help-block m-b-none small character-limit'>" + charLengthInitial + " of " + charLimitInitial + " characters used" + "</span>");
        }
    });

    $("textarea").keyup(function () {

        var charLength = $(this).val().length;
        var charLimit = $(this).data("val-maxlength-max");
        if (charLimit) {
            var characterLimit = $(this).parent(".input-group").nextAll('span.character-limit');

            characterLimit.html(charLength + " of " + charLimit + " characters used");
            if ($(this).val().length > charLimit) {
                characterLimit.html("<strong class='red-text'>You may only have up to " + charLimit + " characters. You currently have " + charLength + "</strong>");
            }
        }
    });

    // date conversion function call with $.date(mydate) returns dd/MM/yyyy
    $.date = function (dateObject) {
        var d = new Date(dateObject);
        var day = d.getDate();
        var month = d.getMonth() + 1;
        var year = d.getFullYear();
        if (day < 10) {
            day = "0" + day;
        }
        if (month < 10) {
            month = "0" + month;
        }
        var date = day + "/" + month + "/" + year;

        return date;
    };

    function getMonth(date) {
        var month = date.getMonth() + 1;
        return month < 10 ? '0' + month : '' + month; // ('' + month) for string result
    }

    function getDay(date) {
        var day = date.getDate();
        return day < 10 ? '0' + day : '' + day;
    }

    function GlobalGenerateDateFromString(date) {
        if (moment(date, "dd/MM/yyyy").isValid()) {
            // extra check to ensure that date parsed is the same as the date entered
            // this eliminates bad dates such as 31st June 2015

            var d = new Date(date.split('/')[2], date.split('/')[1] - 1, date.split('/')[0]);
            var dateString = (getDay(d) + '/' + (getMonth(d)) + '/' + d.getFullYear());

            if (date == dateString) {
                return d;
            }
            else {
                return new Date("Invalid");
            }
        }
        else { return new Date("Invalid"); }
    }

    function clearFilters() {

        $("input[name*='SearchCriteria'][type='text']").each(function () {
            $(this).val("");
        });

        $("input[name*='SearchCriteria'][type='checkbox']").each(function () {
            $(this).prop("checked", false);
        });

        $("input[name*='SearchCriteria'][type='hidden']").each(function () {
            $(this).val("");
            var autoselecter = $(this).siblings('div.auto-selected');
            autoselecter.css("display", "none");
            autoselecter.text("");
            var inputbox = $(this).siblings('input.autocompletebox');
            inputbox.css("display", "inline-block");
        });

        $("select[name*='SearchCriteria']").each(function () {
            $(this).val("");
        });
    }

    function initializeTooltipValidation() {
        try {
            $("form input, form select, form textarea").tooltipValidation({
                placement: "top"
            });
        }
        catch (ex) { }
    }

    function advancedSearch() {

        // Collapse ibox function
        $('.ibox-title.adv-search').click(function () {
            var ibox = $(this).closest('div.ibox');
            var button = $(this).find('i');
            var content = ibox.find('div.ibox-content');
            content.slideToggle(200);
            button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
            ibox.toggleClass('').toggleClass('border-bottom');
            setTimeout(function () {
                ibox.resize();
                ibox.find('[id^=map-]').resize();
            }, 50);
        });

        // If the filter is populated already, keep it open so the user can see what they searched for
        var filterContents = $("#SearchCriteria_Filter").val();
        if (filterContents) {
            $('.ibox-title.adv-search').click();
        }

        $('.orderby').on('click', function () {
            $('#SearchCriteria_Orderby').val($(this).attr("data-id"));
            $(this).parents("form:first").trigger("submit");
            return false;
        });

        $('.page').on('click', function () {
            $('#SearchCriteria_Page').val($(this).attr("data-id"));
            $(this).parents("form:first").trigger("submit");
            return false;
        });

        $(".btn-clear-search").on("click", function () {
            clearFilters();
            $(this).closest("form").trigger("submit");
        });

        $(".btn-advanced-search").on("click", function (e) {
            e.preventDefault();
            $('#SearchCriteria_Page').val(0);
            $(this).parents("form:first").trigger("submit");
            return false;
        });

        $("#PageSize:not([data-ajax])").on("change", function () {
            $('#SearchCriteria_PageSize').val($(this).val());
            $(this).parents("form:first").trigger("submit");
            return false;
        });
    }

    function enableBootstrapTooltips() {
        $('[data-toggle="tooltip"]').tooltip();
    }

    function styleCheckboxes() {
        try {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
        }
        catch (ex) { }
    }

    function renderToastNotification() {
        if ($("#toastr-notification").length) {
            var messageType = $("#toastr-notification").data("type");
            var message = $("#toastr-notification").data("msg");

            toastr.options = {
                "closeButton": true
            }

            if (messageType) {
                toastr[messageType](message);
            } else {
                toastr["info"](message);
            }
        }
    }

    // Extensions
    String.prototype.toTitleCase = function () {
        return this.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
    };
});