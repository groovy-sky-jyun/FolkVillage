
<?php
	require '../../db/db.php';
	
	header('Content-Type: application/json');
	$rawData = file_get_contents("php://input");
	$data = json_decode($rawData, true);

	if (isset($data['userIDPost'])) {
		$user_id = $data['userIDPost'];

		 // 1. user1_id or user2_id == user_id 인 데이터 가져오기
		$sql = "SELECT * FROM messagelist WHERE user1_id = '".$user_id."' or user2_id='".$user_id."'";
		$result = mysqli_query($conn, $sql);	
	
		if(mysqli_num_rows($result)>0){
			while($row = mysqli_fetch_assoc($result)){
				$dataTable[]=array(
					'table_number' => $row['number'],
					'message_count' => $row['message_count']
				);
			}
			$status = 'success';
			$arr = array("status"=>$status, "data"=>$dataTable);
			echo json_encode($arr, JSON_UNESCAPED_UNICODE);
		}
		else{
			$status = 'fail';
			$dataTable[] = array(
				'table_number' => $row['fail'],
				'message_count' => $row['fail']
			);
			$arr = array("status"=>$status, "data"=>$dataTable);
			echo json_encode($arr);
		}
	}
	else echo "useridPost access 실패"
	
?>
