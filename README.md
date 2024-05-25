# Boulderlog

## Base repo: nicholasbergesen/fundamentals

## Purpose
Log boulder sessions


## Useful scripts
### Clear form validation client side
```typescript
$.fn.clearErrors = function () {
    $(this).each(function () {

        $(this).find(".field-validation-error").empty();
        $(this).find(".input-validation-error").removeClass("input-validation-error");
        $(this).trigger('reset.unobtrusiveValidation');
    });
};
$("#create-form").clearErrors();
```