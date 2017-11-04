// $('.btn-group').on('click', '.btn', function() {
//     $(this).addClass('active').siblings().removeClass('active');
//   });
var slider = document.getElementById("myRange");
var output = document.getElementById("demo");
output.innerHTML = slider.value;

slider.oninput = function() {
  output.innerHTML = this.value;
  console.log(this.value);
}