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

