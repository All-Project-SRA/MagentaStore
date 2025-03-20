function getImage(imagename)
{
    var x=document.getElementById("imag");
    var fr=new FileReader();
    fr.readAsDataURL(x.files[0]);
    fr.onloadend=function(imagename){
        var bin=imagename.target.result;
        var img=document.getElementById("img");
        img.src=bin;
    }
}
var expanded = false;

function showCheckboxes() {
  var checkboxes = document.getElementById("checkboxes");
  if (!expanded) {
    checkboxes.style.display = "block";
    expanded = true;
  } else {
    checkboxes.style.display = "none";
    expanded = false;
  }
}
function showCheckboxes1() {
  var checkboxes = document.getElementById("checkboxes1");
  if (!expanded) {
    checkboxes.style.display = "block";
    expanded = true;
  } else {
    checkboxes.style.display = "none";
    expanded = false;
  }
}

