
<?php
	require '../../db/db.php';

	$user_id=$_POST["user_idPost"];
	$friend_id=$_POST["friend_idPost"];
	
	$sql = "SELECT * FROM friendlist WHERE user_id= '".$friend_id."' and  friend_id= '".$user_id."'";
	$result = mysqli_query($conn, $sql);
	if(mysqli_num_rows($result)>0)
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['accept_gift'];
	}
	else
		echo"fail";
	

?>
