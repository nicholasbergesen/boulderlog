# Boulderlog

## Base repo: nicholasbergesen/fundamentals

Changes to the main branch are deployed automatically.

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
