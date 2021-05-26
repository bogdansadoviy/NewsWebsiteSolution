// $(document).on('ready', function () {
//   $('.lazy').slick({
//     lazyLoad: 'ondemand',
//     infinite: true,
//     autoplay: true,
//     autoplaySpeed: 6000,
//     arrows: false,
//     dots: true,
//     draggable: false,
//     fade: false,
//     pauseOnHover: true,
//   })
// })

var acc = document.getElementsByClassName("date");
var d = new Date();

var month = new Array(
    "січня",
    "лютого",
    "березня",
    "квітня",
    "травня",
    "червня",
    "липня",
    "серпня",
    "вересня",
    "жовтня",
    "листопада",
    "грудня"
);

let successDate = d.getDate() + " " + month[d.getMonth()];
acc.innerHTML = successDate;
