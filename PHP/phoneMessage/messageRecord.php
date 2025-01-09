<?php

	require '../../db/db.php';

	header('Content-Type: application/json');
	$rawData = file_get_contents("php://input");
	$data = json_decode($rawData, true);

	if(isset($data['tableNumber'])){
		$table_number = $data['tableNumber'];
	}

	$status = 'fail';
	// sender_id, message_txt 를 message_order 순서대로 가져온다
	$sql = "SELECT sender_id, message_txt FROM messagerecord WHERE list_number = '".$table_number."' order by message_order asc";
	$result = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($result)>0){
		while($row = mysqli_fetch_assoc($result)){
			$dataTable[] =array(
				'sender_id' => $row['sender_id'],
				'message_txt' => $row['message_txt']
			);			
		}
		$status = 'success';
		$arr = array("status"=>$status, "data"=>$dataTable);
		echo json_encode($arr, JSON_UNESCAPED_UNICODE);
	}
	else{
		$dataTable[] = array(
			'sender_id' => $row['fail'],
			'message_txt' => $row['fail']
		);
		$arr = array("status"=>$status, "data"=>$dataTable);
		echo json_encode($arr);
	}
	// 해당 행들을 전부 read check = 1 로 변경한다.
	$sql = "UPDATE messagerecord SET read_check = 1 WHERE list_number = '".$table_number."'";
	$result = mysqli_query($conn, $sql);	
	if(!$result){
		$dataTable[] = array(
			'sender_id' => $row['fail'],
				'message_txt' => $row['fail']
		);
		$arr = array("status"=>$status, "data"=>$dataTable);
		echo json_encode($arr);
	}
?>
