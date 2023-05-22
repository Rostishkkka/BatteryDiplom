<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="stylesheet" href="../css/index.css">
    <link type="text/css" rel="stylesheet" href="../css/profile.css">
    <title>Мой профиль</title>  
</head>
<body>
    <header>
        <div class="dropdown">
            <a id="menu-button"><img width="68px" height="58px" src="../images/header/Menu.png" alt="menu"></a>
            <div class="dropdown-options">
                <a href="index.html">Главная</a>
                <a href="katalog.php">Каталог</a>
                <a href="cart.php">Корзина</a>
                <a href="profile.php">Профиль</a>
                <a href="orders.php">История заказов</a>
            </div>
        </div>
        <a href="index.html" id="header-logo"><img width="200px" height="60px" src="../images/logo.svg" alt="logo">
        <a href="profile.php" id="user-button"><img width="55px" height="55px" src="../images/header/UserPhoto.png" alt="user-icon"></a>
        <a href="cart.php" id="cart-button"><img width="50px" height="50px" src="../images/header/Cart.png" alt="cart"></a>
    </header>

    <div class="wrapper">
        <nav>
            <ul>
                <li> <h3>Информация покупателя</h3> </li>
                <li> <a href="profile.php">Мой профиль</a> </li>
                <li> <a href="orders.php">Мои заказы</a> </li>
                <li> <h3>Помощь</h3> </li>
                <li> <a href="#">Служба поддержки</a> </li>
            </ul>
        </nav>
        <main>
            <h2>Мой профиль</h2>
            <form>
                <div id="form-img">
                    <img width="200px" height="200px" src="..//images/test.jpg">
                </div>
                <div class="form-item">
                    <label>E-mail:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Имя:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Фамилия:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Отчество:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Номер телефона:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Дата рождения:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-item">
                    <label>Пароль:</label>
                    <input type="text" value="trufelnaisveni@gmail.com"></input>
                </div>
                <div class="form-button">
                    <button>Сохранить</button>
                </div>
            </form>
        </main>
    </div>

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
            <a href="#"><img width="40px" height="40px" src="../images/footer/OKIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/VKIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/TelegramIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/YoutubeIcon.png"></a>
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