<?php
	require '../../db/db.php';

	$user_id=$_POST["user_idPost"];
	
	$sql ="SELECT B.nickname, A.send_gift FROM friendlist A LEFT JOIN userinfo B ON A.friend_id=B.id WHERE A.user_id='".$user_id."'"; 
	$result = mysqli_query($conn, $sql);

	if(mysqli_num_rows($result)>0)
	{
		while($row = mysqli_fetch_assoc($result)){
			echo $row['nickname'].','.$row['send_gift'].',';
		}
	} else {
		  echo "null";
		}
?>
