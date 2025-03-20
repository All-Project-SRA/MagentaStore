var sign_in_bth = document.querySelector("#sign-in-bth");
var sign_up_bth = document.querySelector("#sign-up-bth");
var sign_ups_bth = document.querySelector("#sign-ups-bth");
var container = document.querySelector(".container");
var sign_ins_bth =document.querySelector("#sign-ins-bth");
sign_in_bth.addEventListener('click', () => {
    container.classList.remove("sign-up-mode");
});
sign_up_bth.addEventListener('click', () => {
    container.classList.add("sign-up-mode");
});
sign_ups_bth.addEventListener('click', () => {
    container.classList.add("sign-ups-mode");
});
sign_ins_bth.addEventListener('click', () => {
    container.classList.remove("sign-ups-mode");
});
/*اضافة لعرض كلمة السر */
function myFunction(){
    var x=document.getElementById("pass-id");
    var y=document.getElementById("hide01");
    var z=document.getElementById("hide02");
    if(x.type==='password'){
        x.type="text";
        z.style.display='block';
        y.style.display='none';
    }
    else{
        x.type="password";
        z.style.display='none';
        y.style.display='block';
    }
}
function myFunction1(){
    var x=document.getElementById("pass-id1");
    var y=document.getElementById("hide12");
    var z=document.getElementById("hide13");
    if(x.type==='password'){
        x.type="text";
        z.style.display='block';
        y.style.display='none';
    }
    else{
        x.type="password";
        z.style.display='none';
        y.style.display='block';
    }
}
function myFunction2(){
    var x=document.getElementById("pass-id2");
    var y=document.getElementById("hide23");
    var z=document.getElementById("hide24");
    if(x.type==='password'){
        x.type="text";
        z.style.display='block';
        y.style.display='none';
    }
    else{
        x.type="password";
        z.style.display='none';
        y.style.display='block';
    }
}
function myFunction3(){
    var x=document.getElementById("pass-id3");
    var y=document.getElementById("hide34");
    var z=document.getElementById("hide35");
    if(x.type==='password'){
        x.type="text";
        z.style.display='block';
        y.style.display='none';
    }
    else{
        x.type="password";
        z.style.display='none';
        y.style.display='block';
    }
}
function myFunction4(){
    var x=document.getElementById("pass-id4");
    var y=document.getElementById("hide45");
    var z=document.getElementById("hide46");
    if(x.type==='password'){
        x.type="text";
        z.style.display='block';
        y.style.display='none';
    }
    else{
        x.type="password";
        z.style.display='none';
        y.style.display='block';
    }
}
/*اضافة من اجل تحقق كلمة السر*/
function con(){
    var z=document.getElementById("con");
    var x=document.getElementById("pass-id1");
    var y=document.getElementById("pass-id2");
    if(x.value == y.value){
        z.style.border="1px solid #32de8f";
    }else if(y.value===''){
     z.style.border="none";
    }else{
        z.style.border="1px solid red";
    }
};
function con1(){
    var z=document.getElementById("con1");
    var x=document.getElementById("pass-id3");
    var y=document.getElementById("pass-id4");
    if(x.value == y.value){
        z.style.border="1px solid #32de8f";
    }else if(y.value===''){
     z.style.border="none";
    }else{
        z.style.border="1px solid red";
    }
}
/*End*/
/*End*/