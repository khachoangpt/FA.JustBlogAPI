function convertToSlug() {
    var title = document.getElementById('Title').value;
    title = title.replace(/^\s+|\s+$/g, ''); // trim
    title = title.toLowerCase();

    // remove accents, swap ñ for n, etc
    var from = "ãàáäâẽèéëêìíïîõòóöôùúüûñç·/_,:;";
    var to = "aaaaaeeeeeiiiiooooouuuunc------";
    for (var i = 0, l = from.length; i < l; i++) {
        title = title.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    title = title.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-'); // collapse dashes
    document.getElementById('UrlSlug').value = title;
}

$('.time-hide').delay(3000).hide(0);

$(document).ready(function () {
    $('.select-tag-multiple').select2();
});
