# Mobile_A2
Video Link: https://www.youtube.com/watch?v=46LtsvRHpSU

#Idea and Functionality

The app is called SourceTree, I will use this app to store sources of content and to organize the information. The app is able to create an infinite relationship along the tree of topics. Topic -> Subtopic -> subsubtopic etc.. 
The user is able to share their content, including the image by the uploading it to an image hosting site. The app generates a formatted clipboard with the information stored and uploaded link of the image. 
The user can add topics and edit them. I haven't added the delete yet because I wasn't sure the best way to save the child sources. I don't want users to delete an item and lose the entire branch of information.



The web service is a POST to the imgur api (https://api.imgur.com/3/.) for uploading images. I have to include a client ID so that the hosting service knows where the anonymous images are coming from. The API requires the image to be converted to a base64 string and the type uploaded. The response includes the link to the image which is stored locally.

Information stored locally is: Title, Source, Description, ImagePath (device image path), bool (if the topic is a subtopic), Link to online image, foreign key and a list of Topics (subtopics). The table shares a relationship with itself.  
