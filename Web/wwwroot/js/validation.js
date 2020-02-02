$(function () {
    validateInputs();
});

function serverSideValidation() {

    var form = $('form')[0];
    if (!form) return false;

    try {
        var validator = $.data($('form')[0], 'validator');

        if (validator == null) return false;

        var settngs = validator.settings;

        if (settngs == undefined) return false;

        settngs.ignore = ".ignore";

        var errors = $("#validation-list li.validation-list-item");

        if (errors != null && errors.length > 0) {

            $.each(errors, function (key, item) {
                var field = $(item).attr('data-field');
                var message = $(item).attr('data-message');

                var self = $("#" + field);

                var icon = self.siblings(".input-group-addon");
                self.removeClass("valid").addClass("input-validation-error").addClass("ignore");
                self.attr("aria-invalid", "true");

                setInvalidIcon(icon);

                var validation = $(self).parent().siblings(".field-validation-valid");

                validation.removeClass("field-validation-valid");
                validation.addClass("field-validation-error");

                var id = field.replace('.', '_');

                validation.html('<span id=\"' + id + '-error">' + message + '</span>');
            });
        }
    }
    catch (e) {
        // Suppress
    }

    $(".ignore").on("change", function (e) {
        var self = $(this);

        self.removeClass("ignore");

        if (self.val() != "" || self.valid()) return false;

        self.removeClass("valid").addClass("input-validation-error");
        self.attr("aria-invalid", "true");
        var icon = self.siblings(".input-group-addon");
        setInvalidIcon(icon);
        return true;
    });
}

function validateInputs() {
    validateTextBoxes();
    validateTextAreas();
    validateSelects();
    validateSelectpickers();
    validateDatePickers();
    serverSideValidation();
}

function validateTextBoxes() {
    $(".input-group input.form-control[data-val-required]").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group input.form-control:not([data-val-required])").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group input.form-control[data-val-required]").on("change", function (e) {
        var self = $(this);

        if (self.val() != "" || self.valid()) return false;

        self.removeClass("valid").addClass("input-validation-error");
        self.attr("aria-invalid", "true");
        var icon = self.siblings(".input-group-addon");
        setInvalidIcon(icon);
        return true;
    });
}

function validateTextAreas() {
    $(".input-group textarea.form-control[data-val-required]").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group textarea.form-control:not([data-val-required])").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group textarea.form-control[data-val-required]").on("change", function (e) {
        var self = $(this);

        if (self.val() != "" || self.valid()) return false;

        self.removeClass("valid").addClass("input-validation-error");
        self.attr("aria-invalid", "true");
        var icon = self.siblings(".input-group-addon");
        setInvalidIcon(icon);
        return true;
    });
}

function validateSelects() {
    $(".input-group select.form-control[data-val-required]").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group select.form-control:not([data-val-required])").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        setIcon(self, icon);
    });

    $(".input-group select.form-control:not(.selectpicker)[data-val-required]").on("change", function (e) {
        var self = $(this);

        if (self.val() != "" || self.valid()) return false;

        self.removeClass("valid").addClass("input-validation-error");
        self.attr("aria-invalid", "true");
        var icon = self.siblings(".input-group-addon");
        setInvalidIcon(icon);
    });
}

function validateSelectpickers() {
    $(".input-group select.form-control.selectpicker[data-val-required]:not([multiple])").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        try {
            if (self.valid() && self.val() != "") {
                setValidIcon(icon);
            }
            else {
                setInvalidIcon(icon);
            }
        }
        catch (ex) { }
    });

    $(".input-group select.form-control.selectpicker[data-val-required][multiple]").each(function (e) {
        var self = $(this);
        var icon = self.siblings(".input-group-addon");
        try {
            if (self.valid() && (self.val() != "" && self.attr("selectedoptions").length > 0)) {
                setValidIcon(icon);
            }
            else {
                setInvalidIcon(icon);
            }
        }
        catch (ex) { }
    });

    $('.input-group select.form-control.selectpicker:not([data-val-required])').each(function (e) {
        var self = $(this);

        self.addClass("valid").removeClass("input-validation-error");
        self.attr("aria-invalid", "false");
        var icon = self.parents(".input-group").children(".input-group-addon");
        setValidIcon(icon);
    });

    $('.input-group select.form-control.selectpicker[data-val-required]').on('changed.bs.select', function (e) {
        var self = $(this);

        if (self.val() == "" || self.val() == null) {
            self.removeClass("valid").addClass("input-validation-error");
            self.attr("aria-invalid", "true");
            var icon = self.parents(".input-group").children(".input-group-addon");
            setInvalidIcon(icon);
        }
        else {
            self.addClass("valid").removeClass("input-validation-error");
            self.attr("aria-invalid", "false");
            var icon = self.parents(".input-group").children(".input-group-addon");
            setValidIcon(icon);
        }
    });

    $('.input-group select.form-control.selectpicker[multiple]').on('loaded.bs.select', function (e) {
        var self = $(this);

        var selectedValues = self.attr("selectedoptions");

        if (selectedValues == null) return;

        var arr = selectedValues.split(",");

        self.selectpicker('val', arr);

        self.selectpicker('refresh');
    });
}

function validateDatePickers() {
    try {

        $('.input-group.date').datepicker({
            todayBtn: "linked",
            format: "dd/mm/yyyy",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            showOnFocus: false
        }).on("hide", function (e) {

            var self = $(this);

            var input = self.find("input.form-control[data-val-required]:first");

            if (input.val() == "" || !input.valid()) {
                input.removeClass("valid").addClass("input-validation-error");
                input.attr("aria-invalid", "true");
                var icon = input.siblings(".input-group-addon");
                setInvalidIcon(icon);
            }
            else {
                input.addClass("valid").removeClass("input-validation-error");
                input.attr("aria-invalid", "false");
                var icon = input.siblings(".input-group-addon");
                setValidIcon(icon);
            }
        });
    }
    catch (ex) { }
}

function initialiseClockPicker() {
    try {
        $('.clockpicker').clockpicker({
            autoclose: true,
            'default': 'now',
            donetext: "Select",
            placement: 'top',
        });
    }
    catch (ex) { }
}

function setIcon(input, icon) {
    try {
        if (input.valid()) {
            setValidIcon(icon);
        }
        else {
            setInvalidIcon(icon)
        }
    }
    catch (ex) { }
}

function setValidIcon(icon) {
    icon.children('i.fa.validation').removeClass().addClass("fa fa-check-circle green-text validation");
}

function setInvalidIcon(icon) {
    icon.children('i.fa.validation').removeClass().addClass("fa fa-exclamation-circle red-text validation");
}