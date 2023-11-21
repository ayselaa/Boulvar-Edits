
// Language
const selectedLanguage = document.getElementById('selected-language');
const languageOptions = document.getElementById('language-options');

selectedLanguage.addEventListener('click', () => {
    languageOptions.classList.toggle('show');
});

languageOptions.addEventListener('click', (event) => {
    debugger
    if (event.target.tagName === 'LI') {
        const selectedLang = event.target.getAttribute('data-lang');
        // Çerezi güncelle
        document.cookie = "SelectedLanguage=" + selectedLang + "; path=/";
        let selectedLangCookie = getCookie('SelectedLanguage');


        // Seçilen dili güncelle
        selectedLanguage.innerHTML = `${selectedLangCookie} <svg xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 10 6" fill="none">
        <path d="M5.31153 5.8583L9.88116 0.766689C9.95744 0.681733 10 0.568863 10 0.451489C10 0.334116 9.95744 0.221247 9.88116 0.136291L9.876 0.130809C9.83901 0.0894813 9.7945 0.0565729 9.74516 0.0340853C9.69582 0.0115976 9.64269 9.22437e-07 9.589 9.17743e-07C9.5353 9.13049e-07 9.48217 0.0115976 9.43283 0.0340852C9.38349 0.0565729 9.33898 0.0894813 9.302 0.130809L4.99914 4.92549L0.698005 0.130808C0.661024 0.0894805 0.61651 0.0565721 0.567171 0.0340844C0.517831 0.0115968 0.464699 1.20072e-07 0.411006 1.15378e-07C0.357312 1.10684e-07 0.304179 0.0115968 0.254839 0.0340844C0.2055 0.0565721 0.160986 0.0894805 0.124005 0.130808L0.118841 0.13629C0.0425554 0.221246 -4.58347e-07 0.334115 -4.68608e-07 0.451489C-4.78869e-07 0.568862 0.0425553 0.681732 0.118841 0.766688L4.68847 5.8583C4.72866 5.90308 4.77699 5.93872 4.83054 5.96308C4.88409 5.98744 4.94174 6 5 6C5.05826 6 5.11591 5.98744 5.16946 5.96308C5.22301 5.93872 5.27134 5.90308 5.31153 5.8583Z" fill="#CFC273"/>
        </svg>`;
        // Sayfayı yeniden yükle
        window.location.reload();

        if (selectedLangCookie) {
            // Çerezde kayıtlı dili bul ve seçili olarak işaretle
            const selectedLangOption = languageOptions.querySelector(`li[data-lang="${selectedLangCookie}"]`);
            if (selectedLangOption) {
                selectedLangOption.style.display = 'none';
            }
        }

    }
});

document.addEventListener('click', (event) => {
    if (!selectedLanguage.contains(event.target) && !languageOptions.contains(event.target)) {
        languageOptions.classList.remove('show');
    }
});


window.addEventListener('load', () => {
    let selectedLangCookiefunk = getCookie('SelectedLanguage');
    if (selectedLangCookiefunk) {
        selectedLanguage.textContent = selectedLangCookiefunk;
    }
});

function getCookie(name) {
    const cookies = document.cookie.split('; ');
    for (const cookie of cookies) {
        const [cookieName, cookieValue] = cookie.split('=');
        if (cookieName === name) {
            return decodeURIComponent(cookieValue);
        }
    }
    return null;
}


/* Helper function to get a cookie by name*/
function getCookie(name) {
    const cookies = document.cookie.split('; ');
    for (const cookie of cookies) {
        const [cookieName, cookieValue] = cookie.split('=');
        if (cookieName === name) {
            return decodeURIComponent(cookieValue);
        }
    }
    return null;
}


// Pagination 

//document.addEventListener("DOMContentLoaded", function() {
//  const items = document.querySelectorAll(".table-container .tb-item");
//  const itemsPerPage = 3; // Number of items per page
//  let currentPage = 1;

//  function showPage(page) {
//      const start = (page - 1) * itemsPerPage;
//      const end = start + itemsPerPage;

//      items.forEach((item, index) => {
//          if (index >= start && index < end) {
//              item.style.display = "block";
//          } else {
//              item.style.display = "none";
//          }
//      });
//  }

//  showPage(currentPage);

//  const pagination = document.querySelector(".pagination");
//  pagination.addEventListener("click", function(event) {
//      if (event.target.tagName === "A") {
//          event.preventDefault();
//          if (event.target.getAttribute("aria-label") === "Previous") {
//              currentPage = currentPage > 1 ? currentPage - 1 : 1;
//          } else if (event.target.getAttribute("aria-label") === "Next") {
//              const totalPages = Math.ceil(items.length / itemsPerPage);
//              currentPage = currentPage < totalPages ? currentPage + 1 : totalPages;
//          } else {
//              currentPage = parseInt(event.target.textContent);
//          }
//          showPage(currentPage);
//      }
//  });
//});







// Initially set the navigation menu to be closed
document.getElementById("mySidenav").style.width = "0";

function openNav() {
  document.getElementById("mySidenav").style.width = "100%";
  
  var linksSidenav = document.querySelectorAll(".menu_nav_element-div a");
  for (var i = 0; i < linksSidenav.length; i++) {
    linksSidenav[i].style.opacity = "1";
  }

    var boulvardLogo = document.querySelector(".brand-div");
  var sliderpagination = document.querySelector(".slider-pagination");

  document.body.style.setProperty('overflow-y', 'hidden', 'important');
  boulvardLogo.style.zIndex = "0";
  sliderpagination.style.zIndex = "0";
}

function closeNav() {
  document.getElementById("mySidenav").style.width = "0";
  var linksSidenav = document.querySelectorAll(".menu_nav_element-div a");

  for (var i = 0; i < linksSidenav.length; i++) {
    linksSidenav[i].style.opacity = "0";
  }

    var boulvardLogo = document.querySelector(".brand-div");
  var sliderpagination = document.querySelector(".slider-pagination");
  document.body.style.setProperty('overflow-y', 'auto', 'important');

  // "Boulevard Logo" elemanının z-index özelliğini 1 saniye sonra değiştirin
  setTimeout(function() {
    boulvardLogo.style.zIndex = "5"; // veya istediğiniz z-index değeri
  }, 1000); // 1000 milisaniye (1 saniye) bekleyin
  sliderpagination.style.zIndex = "3"

  }

window.addEventListener("resize", function() {
  if (window.innerWidth > 480) {
    document.getElementById("mySidenav").style.width = "0";
  }
});

// MODAL Js
const openModalBtn = document.getElementById("openModal");
const closeModalBtn = document.getElementById("closeModalll");
const modal = document.getElementById("myModalNumber");
// const navbar = document.querySelector(".navbar");
const body = document.body;

// Function to open the modal

function openModal() {
  modal.style.display = "block";
  // navbar.style.zIndex = "0";
  // body.style.overflow = "hidden";
}

// Function to close the modal
function closeModal() {
  modal.style.display = "none";
  // navbar.style.zIndex = "2";
  // body.style.overflow = "auto";
}

// Attach click event listeners to open and close the modal
if(openModalBtn != null){
  openModalBtn.addEventListener("click", openModal);
}
if(closeModalBtn != null){
  closeModalBtn.addEventListener("click", closeModal);
}

// Slick Slider

function createSlick() {
  $(".slider")
    .not(".slick-initialized")
    .slick({
      centerMode: false,
      autoplay: true,
     autoplaySpeed: 2000,
      slidesToShow: 1.9,
      responsive: [
        {
          breakpoint: 768,
          settings: {
            dots: false,
            arrows: false,
            infinite: false,
            slidesToShow: 1,
            slidesToScroll: 1,
          },
        },
      ],
    });
}

createSlick();

$(window).on("resize", createSlick);


// Arc-Elegance Animations

var $animation_elements = $(".animation-element");
var $window = $(window);

function check_if_in_view() {
  var window_height = $window.height();
  var window_top_position = $window.scrollTop();
  var window_bottom_position = window_top_position + window_height;

  $.each($animation_elements, function () {
    var $element = $(this);
    var element_height = $element.outerHeight();
    var element_top_position = $element.offset().top;
    var element_bottom_position = element_top_position + element_height;

    //check to see if this current container is within viewport
    if (element_top_position <= window_bottom_position) {
      $element.addClass("in-view");
    } else {
      $element.removeClass("in-view");
    }
  });
}

$window.on("scroll resize", check_if_in_view);
$window.trigger("scroll");


// Blog
$(document).ready(function(){
  $('.carousell-slick').slick({
    speed: 500,
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 25000,
    dots:true,
    centerMode: true,
    responsive: [{
      breakpoint: 1024,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,

      }

    }, {
      breakpoint: 800,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 2,
        dots: true,
        infinite: true,

      }
    },  {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: true,
        infinite: true,
        autoplay: true,
        autoplaySpeed: 2000,
      }
    }]
  });
});


//Buildings
document.addEventListener("DOMContentLoaded", function () {
  const buildingLinks = document.querySelectorAll(".building__link");

  buildingLinks.forEach((link) => {
    const dataId = link.getAttribute("data-id");
    const buildingInfo = document.querySelector(`.building__info[data-id="${dataId}"]`);
    const buildingRect = buildingInfo.querySelector("rect:nth-child(1)");
    const buildingRect2 = buildingInfo.querySelector("rect:nth-child(2)");

    link.addEventListener("mouseover", () => {
      buildingInfo.style.display = "block";
      buildingRect.style.display = "block";
      buildingRect2.style.display = "block";
      buildingRect2.classList.add("fade-in-animation");


      const st12Elements = buildingInfo.querySelectorAll(".st12");
      st12Elements.forEach((st12Element) => {
        st12Element.style.display = "block";
      });

      const st0Elements = buildingInfo.querySelectorAll(".st0");
      st0Elements.forEach((st0Element) => {
        st0Element.style.display = "block";
      });
    });

    link.addEventListener("mouseout", () => {
      buildingInfo.style.display = "block";
      buildingRect.style.display = "none";
      buildingRect2.style.display = "block";
      buildingRect2.classList.remove("fade-in-animation");


      const st12Elements = buildingInfo.querySelectorAll(".st12");
      st12Elements.forEach((st12Element) => {
        st12Element.style.display = "none";
      });

      const st0Elements = buildingInfo.querySelectorAll(".st0");
      st0Elements.forEach((st0Element) => {
        st0Element.style.display = "none";
      });
    });
  });
});


// Choose Aparments link ==>  Aparment Section
document.querySelector('.go-aparment-section').addEventListener('click', function(event) {
  event.preventDefault(); // Prevent the default behavior of the link
  const apartmentSection = document.getElementById('apartment-section');
  apartmentSection.scrollIntoView({ behavior: 'smooth' });
});


// Go to Top Button
var scrollToTopButton = document.getElementById("scrollToTopButton");

window.onscroll = function() {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
    scrollToTopButton.style.display = "block";
  } else {
    scrollToTopButton.style.display = "none";
  }
};

scrollToTopButton.addEventListener("click", function() {
  document.body.scrollTop = 0; // Safari
  document.documentElement.scrollTop = 0; // Diger brauzerler
});
// Send Request , Pdf Sidebar
document.addEventListener('DOMContentLoaded', () => {

  const sidebarToggle = document.getElementById('sidebarToggle');
  const sidebarPdfToggle = document.getElementById('sidebarTogglePdf');
  const overlay = document.getElementById('overlay');
  const sidebar = document.getElementById('sidebar-p');
  const sidebarPdf = document.getElementById('sidebar-pdf');
  const closeLinksidebar = document.getElementsByClassName('close-sidebar-link');
  
if(sidebarToggle != null){
  sidebarToggle.addEventListener('click', () => {
    sidebar.style.right = '0px';
    overlay.style.display = 'block';
});
}
if(sidebarToggle != null){
  sidebarPdfToggle.addEventListener('click', () => {
      sidebarPdf.style.right = '0px';
      overlay.style.display = 'block';
  });
}
  
  for (let i = 0; i < closeLinksidebar.length; i++) {
    closeLinksidebar[i].addEventListener('click', (event) => {
        event.preventDefault();
        sidebar.style.right = '-400px';
        sidebarPdf.style.right = '-400px';
        overlay.style.display = 'none';
    });
  }
});


// Line and Ellipse Group Move
var hareketliGrup = document.getElementById("hareketliGrup");

var yeniX = 280;
var yeniY = 460;

hareketliGrup.setAttribute("transform", "translate(" + yeniX + "," + yeniY + ")");





//Apartment Dropdowns
var openDropdown = null;

function toggleDropdown(dropdownNumber) {
  var dropdownContent = document.getElementById("myDropdown" + dropdownNumber);

  if (openDropdown && openDropdown !== dropdownContent) {
    openDropdown.classList.remove("show-dropdown");
  }

  dropdownContent.classList.toggle("show-dropdown");
  openDropdown = dropdownContent;
}
document.querySelector('.dropdown-button:nth-child(2)').addEventListener('click', function() {
  toggleDropdown(2);
});
document.querySelector('.dropdown-button:nth-child(3)').addEventListener('click', function() {
  toggleDropdown(3);
});
document.querySelector('.dropdown-button:nth-child(4)').addEventListener('click', function() {
  toggleDropdown(4);
});
document.querySelector('.dropdown-button:nth-child(5)').addEventListener('click', function() {
  toggleDropdown(5);
});

// Bu fonksiyon, checkbox içindeki tıklamaları engeller.
function handleCheckboxClick(event) {
  event.stopPropagation();
}

// Checkbox etkinliğini izlemek için her bir checkboxa bir dinleyici ekleyin.
var checkboxes = document.querySelectorAll(".custom-checkbox input[type='checkbox']");
checkboxes.forEach(function(checkbox) {
  checkbox.addEventListener('click', handleCheckboxClick);
});

// Dropdown içerisinde herhangi bir yere tıklanırsa dropdown'un kapanmamasını sağlar.
document.querySelector('.dropdown-filter').addEventListener('click', function(event) {
  event.stopPropagation();
});

// Sayfa üzerinde herhangi bir yere tıklanırsa dropdown'u kapatır.
document.addEventListener('click', function(event) {
  var dropdowns = document.getElementsByClassName("dropdown-content-item");
  for (var i = 0; i < dropdowns.length; i++) {
    var dropdownContent = dropdowns[i];
    if (dropdownContent.classList.contains('show-dropdown')) {
      dropdownContent.classList.remove('show-dropdown');
    }
  }
  openDropdown = null;
});

