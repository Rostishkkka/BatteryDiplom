<?php
// function getPhoto(){
    //     $tsql = "SELECT userPhoto FROM [user] WHERE idUser =" . $_COOKIE["idUser"];
    //     $stmt = sqlsrv_query($conn, $tsql);
    //     if ($stmt === false) {
    //         echo "Ошибка, сервис временно недоступен.</br>";
    //         die(print_r(sqlsrv_errors(), true));
    //     }
    //     $row = sqlsrv_fetch_array($stmt);
    //     if($row[0] != false){
    //          echo ' <a alt="user-icon" href="index.php?action=account" id="user-button"><img width="55px" height="55px" src="data:image/jpeg;base64,' . base64_encode($row[0]) . '"></a>';
    //     }
    //        else{
    //          echo ' <a href="index.php?action=account" id="user-button"><img width="55px" height="55px" src="../images/header/UserPhoto.png" alt="user-icon"></a>';
    //     }
    // }

session_start();
if (isset($_POST["loginAuth"])) {
    $serverName = "26.159.241.191";
    $uid = "da";
    $pwd = "da";
    $connectionInfo = array(
        "UID" => $uid,
        "PWD" => $pwd,
        "Database" => "Batteries",
        "CharacterSet" => "UTF-8"
    );
    $conn = sqlsrv_connect($serverName, $connectionInfo);
    if ($conn === false) {
        echo "Ошибка, сервис временно недоступен.</br>";
        die(print_r(sqlsrv_errors(), true));
    }
    $log = $_POST['loginAuth'];
    $pass = $_POST['passwordAuth'];
    $tsql = "SELECT idUser FROM [user] WHERE (login='" . $log . "' OR phoneNumber='" . $log . "') AND password='" . $pass . "'";
    $stmt = sqlsrv_query($conn, $tsql);
    if ($stmt === false) {
        echo "Ошибка, сервис временно недоступен.</br>";
        die(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt);
    if ($row) {
        setcookie("idUser", $row[0]);
        echo "<script>window.location.href = 'index.php';</script>";
    } else {
        echo '<script>alert("Введены неверные логин и/или пароль");</script>';
    }
    sqlsrv_close($conn);
}
?>
<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="stylesheet" href="../css/index.css">
    <link type="text/css" rel="stylesheet" href="../css/authorization.css   ">
    <link type="text/css" rel="stylesheet" href="../css/itc-slider.css">
    <link type="text/css" rel="stylesheet" href="../css/photo.css">
    <script src="../js/itc-slider.js" defer></script>
    <script src="../js/itc-tabs.js" defer></script>
    <script src="../js/itc-photo.js" defer></script>
    <script src="../js/itc-password.js" defer></script>
    <title>Battery</title>
</head>

<body>
    <header>
        <div class="dropdown">
            <a id="menu-button"><img width="68px" height="58px" src="../images/header/Menu.png" alt="menu"></a>
            <div class="dropdown-options">
                <a href="index.php">Главная</a>
                <a href="katalog.php">Каталог</a>
                <a href="cart.php">Корзина</a>
                <a href="#">Профиль</a>
                <a href="orders.php">История заказов</a>
            </div>
        </div>
        <a href="index.php" id="header-logo"><img width="200px" height="60px" src="../images/logo.svg" alt="logo">
        <!-- Выгрузить фото -->
        <a href="#" id="user-button"><img width="55px" height="55px" src="../images/header/UserPhoto.png" alt="user-icon"></a>
        <a href="cart.php" id="cart-button"><img width="50px" height="50px" src="../images/header/Cart.png" alt="cart"></a>
    </header>

    <main>
        <div class="tabs">
            <div class="tabs__nav">
                <button class="tabs__btn tabs__btn_active">Регистрация</button>
                <button class="tabs__btn">Авторизация</button>
            </div>
            <div class="tabs__content">
<div class="tabs__pane tabs__pane_show">
                <!-- первая страница -->
                <form>
                    <div id="form-img">
                        <label for="pct" id="photo"></label>
                        <input type="file" accept="image/jpeg,image/png" id="pct">
                    </div>
                    <div class="form-item">
                        <p>E-mail:</p>
                        <input type="text" ></input>
                    </div>
                    <div class="form-item">
                        <p><span class="warning">*</span>Имя:</p>
                        <input type="text"></input>
                    </div>
                    <div class="form-item">
                        <p>Фамилия:</p>
                        <input type="text" ></input>
                    </div>
                    <div class="form-item">
                        <p>Отчество:</p>
                        <input type="text" ></input>
                    </div>
                    <div class="form-item">
                        <p>Номер телефона:<span class="warning">*</span></p>
                        <input type="text" ></input>
                    </div>
                    <div class="form-item">
                        <p>Дата рождения:</p>
                        <input type="date" ></input>
                    </div>
                    <div class="form-item">
                        <p><span class="warning">*</span>Пароль:</label>                            
                        <input type="password" id="password">
                        <button class="btn btn-primary btn-md" id="show"><img src="../images/authorization/closeEye.png" id="show-img" class="show-img" width="15px" height="15px" alt="Кнопка «button»"></button>
                    </div>
                    <div class="form-item">
                        <p><span class="warning">*</span>Повторите пароль:</label>
                        <input type="password" id="password1">
                        <button  class="btn btn-primary btn-md" id="show1"><img src="../images/authorization/closeEye.png" id="show-img1" class="show-img" alt="Кнопка «button»"></button>
                    </div>
                    <div class="form-button">
                        <button id="atuin-btn">Зарегистрироваться</button>
                    </div>
                </form>
              </div>
              <div class="tabs__pane">
                <!-- вторая страница -->
                <form method="post">
                    <div class="form-item">
                        <p><span class="warning">*</span>Почта/номер телефона:</p>
                        <input type="text" name="loginAuth"></input>
                    </div>
                    <div class="form-item">
                        <p><span class="warning">*</span>Пароль:</label>                            
                        <input type="password" id="password2" name="passwordAuth">
                        <button class="btn btn-primary btn-md" id="show2"><img src="../images/authorization/closeEye.png" id="show-img2" class="show-img" width="15px" height="15px" alt="Кнопка «button»"></button>
                    </div>
                    <div class="form-button">
                        <a><button>Войти</button></a>
                    </div>
                </form>           
              </div>
            </div>
        </div>

    </main>

    <footer>
        <div id="left-footer">
            <p>Задать вопрос:</p>
            <p>8 (903) 963-08-61</p>
            <p>trufelnaisveni@gmail.com</p>
        </div>
        <div id="center-footer">
            <img width="100px" height="30px" src="../images/logo.svg">
        </div>
        <div id="right-footer">
            <p>Мы в социальных сетях:</p>
            <a href="https://ok.ru/"><img width="40px" height="40px" src="../images/footer/OKIcon.png"></a>
            <a href="https://vk.com/"><img width="40px" height="40px" src="../images/footer/VKIcon.png"></a>
            <a href="https://web.telegram.org/"><img width="40px" height="40px" src="../images/footer/TelegramIcon.png"></a>
            <a href="https://www.youtube.com/"><img width="40px" height="40px" src="../images/footer/YoutubeIcon.png"></a>
        </div>
        <div id="botton-footer">
            <p>Обращаем Ваше внимание на то, что все объявления о
                моделях аккумуляторов, размещенные на настоящем интернет-сайте,
                носят исключительно информационный характер и ни при каких условиях не являются публичной офертой,
                определяемой положениями Статьи 437 Гражданского кодекса Российской Федерации.
                Для получения точной информации о наличии модели с требуемой комплектацией и техническими характеристиками,
                пожалуйста, обращайтесь к менеджерам по продажам. <br><br>
                Вы принимаете условия политики конфиденциальности и пользовательского соглашения каждый раз,
                когда оставляете свои данные в любой форме обратной связи.</p>
        </div>
    </footer>
</body>

</html>