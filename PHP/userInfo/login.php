<?php
	require '../../db/db.php';

	$id=$_POST["idPost"];
	$password = $_POST["passwordPost"];
	
	$sql = "SELECT * FROM userinfo WHERE id = '".$id."' ";
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result)>0)
	{
		while ($row = mysqli_fetch_assoc($result)) {
		  if($row['pw'] == $password)
		  {
			echo $row['nickname'];
		  }else {
			echo "login fail";
		  }
		}
	} else 
	{
		echo "login fail";
	}

?>
