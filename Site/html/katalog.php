<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link type="text/css" rel="stylesheet" href="../css/index.css">
    <link type="text/css" rel="stylesheet" href="../css/katalog.css">
    <title>Katalog</title>
</head>
<body>
    <header>
        <div class="dropdown">
            <a id="menu-button"><img width="68px" height="58px" src="../images/header/Menu.png" alt="menu"></a>
            <div class="dropdown-options">
                <a href="#">Главная</a>
                <a href="katalog.html">Каталог</a>
                <a href="katalog.html">Корзина</a>
                <a href="katalog.html">Профиль</a>
                <!-- TODO: отображение поверх слайдера -->
            </div>
        </div>
        <a href="#" id="header-logo"><img width="200px" height="60px" src="../images/logo.svg" alt="logo">
        <a href="#" id="user-button"><img width="55px" height="55px" src="../images/header/UserPhoto.png" alt="user-icon"></a>
        <a href="cart.html" id="cart-button"><img width="50px" height="50px" src="../images/header/Cart.png" alt="cart"></a>
    </header>

    <main>
        <div class="cards-header">
            <div class="sort">
                <p>Сортировать по </p>
                <select>
                    <option>алфавиту (А-Я)</option>
                    <option>алфавиту (Я-А)</option>
                    <option>возрастанию цены</option>
                    <option>убыванию цены</option>
                </select>
            </div>
            <div class="search">
                <img width="19px" height="19px" src="../images/katalog/SearchIcon.png">
                <input type="text">
            </div>
        </div>
        <div class="cards">
            <?php
                $serverName = "26.159.241.191";
                $uid = "da";
                $pwd = "da";
                $connectionInfo = array( "UID"=>$uid,  
                                        "PWD"=>$pwd,  
                                        "Database"=>"Batteries",
                                        "CharacterSet"=>"UTF-8"
                                        );
                $conn = sqlsrv_connect( $serverName, $connectionInfo);  
                if( $conn === false )  
                {  
                    echo "Ошибка, сервис временно недоступен.</br>";  
                    die( print_r( sqlsrv_errors(), true));  
                }

                $tsql = "SELECT idBatteries, nameBatteries, priceBatteries, photoBatteries FROM menu";
                $stmt = sqlsrv_query($conn, $tsql);
                if( $stmt === false )  
                {  
                    echo "Ошибка, сервис временно недоступен.</br>";  
                    die( print_r( sqlsrv_errors(), true));  
                }

                do {
                    $row = sqlsrv_fetch_array($stmt);
                    if ($row) {
                        echo '
                            <div class="card">
                                <img width="298px" height="298px" src="data:image/jpeg;base64,'.base64_encode($row[3]).'">
                                <p class="stuff-name">'.$row[1].'</p>
                                <p class="price">'.$row[2].' руб.</p>
                                <button id="add" idStaff='.$row[0].'>Купить</button>
                            </div>
                        ';
                    }
                } while ($row);
                sqlsrv_free_stmt( $stmt);  
                sqlsrv_close( $conn);  
            ?>
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
            <a href="#"><img width="40px" height="40px" src="../images/footer/OKIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/VKIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/TelegramIcon.png"></a>
            <a href="#"><img width="40px" height="40px" src="../images/footer/YoutubeIcon.png"></a>
        </div>
        <div id="botton-footer">
            <p>Обращаем Ваше внимание на то, что все объявления о 
                моделях автомобилей, размещенные на настоящем интернет-сайте, 
                носят исключительно информационный характер и ни при каких условиях не являются публичной офертой, 
                определяемой положениями Статьи 437 Гражданского кодекса Российской Федерации. 
                Для получения точной информации о наличии моделей с требуемой комплектацией и техническими характеристиками,
                 пожалуйста, обращайтесь к менеджерам по продажам. <br><br>
                Вы принимаете условия политики конфиденциальности и пользовательского соглашения каждый раз,
                 когда оставляете свои данные в любой форме обратной связи.</p>
        </div>
    </footer>
</body>
</html>