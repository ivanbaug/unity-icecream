var camera1 : Camera; 
var camera2 : Camera; 
 
function Start () { 
   camera1.enabled = true; 
   camera2.enabled = false; 
} 
 
function Update () { 
   if (Input.GetKeyDown ("c")){ 
      camera1.enabled = false; 
      camera2.enabled = true; 
   } 
   if (Input.GetKeyDown ("v")){ 
      camera1.enabled = true; 
      camera2.enabled = false; 
   }     
}