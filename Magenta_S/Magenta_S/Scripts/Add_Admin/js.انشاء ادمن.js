function myFunction(){
    var x=document.getElementById("pass-id");
    var y=document.getElementById("hide1");
    var z=document.getElementById("hide2");
    var c=document.getElementById("con");
    if(x.type==='password')
    {
        x.type="text";
        y.style.display="none";
        z.style.display="block";
    }else{
        x.type="password";
        y.style.display="block";
        z.style.display="none";
    }
};
function myFunction1(){
    var x=document.getElementById("pass-id1");
    var y=document.getElementById("hide3");
    var z=document.getElementById("hide4");
    if(x.type==='password')
    {
        x.type="text";
        y.style.display="none";
        z.style.display="block";
    }else{
        x.type="password";
        y.style.display="block";
        z.style.display="none";
    }
};
function con(){
    var y=document.getElementById("pass-id");
    var z=document.getElementById("pass-id1");
    if(y.value == z.value){
        z.style.borderBottom="2px solid #32de8f";
    }else if(z.value===''){
        z.style.borderBottom="2px solid red";
    }else{
        z.style.borderBottom="2px solid red";
    }
};