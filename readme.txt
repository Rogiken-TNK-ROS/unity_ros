ROS側起動手順
1.ROS側のPCにRosBridge Serverをインストール．

$ sudo apt-get install ros-kinetic-rosbridge-server

2.RosBridge Serverを立ち上げる。

$ roslaunch rosbridge_server rosbridge_websocket.launch

（ここでいったん，Unity側へ）

3.ROS側のPCのターミナルにClient connectedと表示されたら接続完了


Unity側起動手順
1.Unity起動

2.RosMeshProject（WRS_TNK/unity_ros/RosMeshProject)をclone
  (*こまったら新しいUnityProjectを適当に作って，WRS_TNK/unity_ros/SubscribeMeshOrPCL.unitypackageをImportしてください)
  
3.plojectビューにあるAssetsフォルダの直下にMesh_testシーンがあるのでopen

4.hieraruchyビューにあるMeshCreaterをクリックしてinspectorビューの一番左上にチェックが入ってることを確認

5.inspectorビューの中段にRosConnectorがあるのでRosBridgeServerUrlの部分にROS側のPCのIPを入力

6.ROS側からMeshをfloat32arrayでpublishする場合は[FloatArraySubsciber]にチェック
  ROS側からPointCloud2をpublishする場合は[PointClouSubsciber]にチェック
  
7.それぞれのTopicの部分にPublisherのtopic名を入力

8.画面上部のPlayボタンをクリック．

*新しいProjectを作った場合，一つ設定をいじる必要があります
　1.上部についてるメニューからEdit > Project Settings > PlayerをOpen
  2.Inspectorビューに設定画面が出てくるのでOther Settings > Configurationを見る
　3.Scripting Runtime Versionが .Net 4.x Equivalent　になっていれば大丈夫

