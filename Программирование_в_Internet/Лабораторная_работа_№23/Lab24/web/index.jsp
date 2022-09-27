<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
  <title>$Title$</title>
  <style>
    :not(:active) > a.DoubleClick1:not(:hover) {
      background-color: #4CAF50; /* Green */
      border: none;
      color: white;
      padding: 15px 32px;
      text-align: center;
      text-decoration: none;
      display: inline-block;
      font-size: 16px;
      margin: 4px 2px;
      cursor: pointer;
      pointer-events: none;
    }
    li, ul{
      list-style-type: none;
      margin: 5px;
    }
    .custom-btn {
      width: 130px;
      height: 40px;
      color: #fff;
      border-radius: 5px;
      padding: 10px 25px;
      font-family: 'Lato', sans-serif;
      font-weight: 500;
      background: transparent;
      cursor: pointer;
      transition: all 0.3s ease;
      position: relative;
      display: inline-block;
      box-shadow:inset 2px 2px 2px 0px rgba(255,255,255,.5),
      7px 7px 20px 0px rgba(0,0,0,.1),
      4px 4px 5px 0px rgba(0,0,0,.1);
      outline: none;
    }
    /* 3 */
    .btn-3 {
      background: rgb(0,172,238);
      background: linear-gradient(0deg, rgba(0,172,238,1) 0%, rgba(2,126,251,1) 100%);
      width: 130px;
      height: 40px;
      line-height: 42px;
      padding: 0;
      border: none;

    }
    .btn-3 span {
      position: relative;
      display: block;
      width: 100%;
      height: 100%;
    }
    .btn-3:before,
    .btn-3:after {
      position: absolute;
      content: "";
      right: 0;
      top: 0;
      background: rgba(2,126,251,1);
      transition: all 0.3s ease;
    }
    .btn-3:before {
      height: 0%;
      width: 2px;
    }
    .btn-3:after {
      width: 0%;
      height: 2px;
    }
    .btn-3:hover{
      background: transparent;
      box-shadow: none;
    }
    .btn-3:hover:before {
      height: 100%;
    }
    .btn-3:hover:after {
      width: 100%;
    }
    .btn-3 span:hover{
      color: rgba(2,126,251,1);
    }
    .btn-3 span:before,
    .btn-3 span:after {
      position: absolute;
      content: "";
      left: 0;
      bottom: 0;
      background: rgba(2,126,251,1);
      transition: all 0.3s ease;
    }
    .btn-3 span:before {
      width: 2px;
      height: 0%;
    }
    .btn-3 span:after {
      width: 0%;
      height: 2px;
    }
    .btn-3 span:hover:before {
      height: 100%;
    }
    .btn-3 span:hover:after {
      width: 100%;
    }
    .buttonFile {background-color: #e7e7e7; color: black;} /* Gray */
  </style>
  <style>
    /* Стили всплывающего окна по-умолчанию */
    .modal {
      position: fixed; /* фиксированное положение */
      top: 0;
      right: 0;
      bottom: 0;
      left: 0;
      background: rgba(0,0,0,0.5); /* фон */
      z-index: 1050;
      opacity: 0; /* по умолчанию модальное окно прозрачно */
      -webkit-transition: opacity 200ms ease-in;
      -moz-transition: opacity 200ms ease-in;
      transition: opacity 200ms ease-in; /* анимация перехода */
      pointer-events: none; /* элемент невидим для событий мыши */
      margin: 0;
      padding: 0;
    }
    /* При отображении модального окно */
    .modal:target {
      opacity: 1; /* делаем окно видимым */
      pointer-events: auto; /* элемент видим для событий мыши */
      overflow-y: auto; /* добавляем прокрутку по y, когда элемент не помещается на страницу */
    }
    /* ширина модального окна и его отступы от экрана */
    .modal-dialog {
      position: relative;
      width: auto;
      margin: 10px;
    }
    @media (min-width: 576px) {
      .modal-dialog {
        max-width: 500px;
        margin: 30px auto; /* отображение окна по центру */
      }
    }
    /* Стили для блока с контентом окна */
    .modal-content {
      position: relative;
      display: -webkit-box;
      display: -webkit-flex;
      display: -ms-flexbox;
      display: flex;
      -webkit-box-orient: vertical;
      -webkit-box-direction: normal;
      -webkit-flex-direction: column;
      -ms-flex-direction: column;
      flex-direction: column;
      background-color: #fff;
      -webkit-background-clip: padding-box;
      background-clip: padding-box;
      border: 1px solid rgba(0,0,0,.2);
      border-radius: .3rem;
      outline: 0;
    }
    @media (min-width: 768px) {
      .modal-content {
        -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
        box-shadow: 0 5px 15px rgba(0,0,0,.5);
      }
    }
    /* Стили заголовка окна */
    .modal-header {
      display: -webkit-box;
      display: -webkit-flex;
      display: -ms-flexbox;
      display: flex;
      -webkit-box-align: center;
      -webkit-align-items: center;
      -ms-flex-align: center;
      align-items: center;
      -webkit-box-pack: justify;
      -webkit-justify-content: space-between;
      -ms-flex-pack: justify;
      justify-content: space-between;
      padding: 15px;
      border-bottom: 1px solid #eceeef;
    }
    .modal-title {
      margin-top: 0;
      margin-bottom: 0;
      line-height: 1.5;
      font-size: 1.25rem;
      font-weight: 500;
    }
    /* Стили кнопки "х" ("Закрыть")  */
    .close {
      float: right;
      font-family: sans-serif;
      font-size: 24px;
      font-weight: 700;
      line-height: 1;
      color: #000;
      text-shadow: 0 1px 0 #fff;
      opacity: .5;
      text-decoration: none;
    }
    /* Стили для закрывающей кнопки в фокусе или наведении */
    .close:focus, .close:hover {
      color: #000;
      text-decoration: none;
      cursor: pointer;
      opacity: .75;
    }
    /* Стили блока основного содержимого окна */
    .modal-body {
      position: relative;
      -webkit-box-flex: 1;
      -webkit-flex: 1 1 auto;
      -ms-flex: 1 1 auto;
      flex: 1 1 auto;
      padding: 15px;
      overflow: auto;
    }
  </style>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js"></script>
  <script src="FileManager.js"></script>
  <link rel="icon" href="data:,">
</head>
<body>
<!-- HTML модального окна -->
<div id="openModal" class="modal">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h3 class="modal-title">Input name directory</h3>
        <a href="#close" title="Close" class="close">×</a>
      </div>
      <div class="modal-body">
        <form action="#" method="get" onsubmit="return false">
          <label>new dir: <input type="text" name="x"/></label>
          <button onclick="CreateNewDir(this.form.x)" href="#close" title="Close" class="close">Create</button>
        </form>

      </div>
    </div>
  </div>
</div>
<!-- HTML кнопки -->

<a style="margin-right: 20px;" href="#openModal">Create new directory</a>
<a style="margin-right: 20px;" href="#" onclick="RemoveFileOrDir()">Remove</a>
<a style="margin-right: 20px;" href="#" onclick="DownloadFileOrDir()">Download</a>
<a style="margin-right: 20px;" href="#" onclick="saveCopyFiles()">Copy Files</a>
<a style="margin-right: 20px;" href="#" onclick="CopyFiles()">Insert Files</a>
<!--<button type="submit" style="margin-right: 20px;" href="#" onclick="DownloadFileOrDir()">Download file</button>-->

<form action="./FileManager" method="post" enctype="multipart/form-data">
  <input type="hidden" name="type" value="UploadFile" />
  <label>File name: <input type="file" name="file"/></label>
  <button type="submit">Add file</button>
</form>
<br>


<!-- CSS-стили модального окна -->


<button style="margin-left: 50px; width: 150px; height: 150px" ondblclick="DownLevelDirectory(value)">
  <figure class="sign"><p>
    <img style="width: 70px; height: 70px; " src="https://png.pngtree.com/png-vector/20190916/ourlarge/pngtree-folder-icon-for-your-project-png-image_1731108.jpg"/>
  </p><figcaption>..</figcaption>
  </figure>
</button>
<ul id="fileAndDirectory">

</ul>
<script>UpdateFilesAndDirectorys("")</script>
</body>
</html>
