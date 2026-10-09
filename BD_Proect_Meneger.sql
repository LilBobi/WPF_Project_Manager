drop database is1_25_kokorinds_Kursovoy_Project
create database is1_25_kokorinds_Kursovoy_Project
use is1_25_kokorinds_Kursovoy_Project

create table Post
(
id_Post int primary key identity,
Namee nvarchar(50) not null,
)

create table Rolee
(
id_Role int primary key identity,
Namee nvarchar(50) not null,
)

create table Userr
(
id_User int primary key identity,
Surname nvarchar(50) not null,
Namee nvarchar(50) not null,
Patronymic nvarchar(50) not null,
id_Post int,
Email nvarchar(50) not null,
Number_Phone nvarchar(50) not null,
Passwordd nvarchar(50) not null,
id_Role int not null,
foreign key (id_Post) references Post(id_Post),
foreign key (id_Role) references Rolee(id_Role),
)

create table Eventt
(
id_Event int primary key identity,
id_User int not null,
Namee nvarchar(150) not null,
Descriptionn nvarchar(150) not null,
Start_Datee date not null,
End_Date date not null,
Place nvarchar(50) not null,
foreign key (id_User) references Userr(id_User),
)

create table News
(
id_News int primary key identity,
Heading nvarchar(200) not null,
Descriptionn nvarchar(200) not null,
Publication_Date date not null,
id_User int not null,
foreign key (id_User) references Userr(id_User),
)

create table Priorityy
(
id_Priority int primary key identity,
Namee nvarchar(50) not null,
)

create table Statuss
(
id_Status int primary key identity,
Namee nvarchar(50) not null,
)

create table Resource_Type
(
id_Resource_Type int primary key identity,
Namee nvarchar(50) not null,
)

create table Resourcee
(
id_Resource int primary key identity,
id_Resource_Type int,
Namee nvarchar(50),
Descriptionn nvarchar(250),
Availabilityy nvarchar(250),
Foreign key (id_Resource_Type) references Resource_Type(id_Resource_Type),
)

create table Production_Project
(
id_Project int primary key identity,
Namee nvarchar(100) not null,
Descriptionn nvarchar(200),
Start_Datee date not null,
End_Datee date,
Budget money not null,
id_Status int not null,
id_Resource int not null,
Foreign key (id_Status) references Statuss(id_Status),
Foreign key (id_Resource) references Resourcee(id_Resource),
)

create table Documentation
(
id_Documentation int primary key identity,
id_Status int not null,
id_Project int not null,
Progress nvarchar(50),
Cost money,
Profit money,
Foreign key (id_Status) references Statuss(id_Status),
Foreign key (id_Project) references Production_Project(id_Project),
)

create table Project_Team
(
id_Project_Team int primary key identity,
id_Project int,
id_User int,
Foreign key (id_Project) references Production_Project(id_Project),
Foreign key (id_User) references Userr(id_User),
)

create table Tasks
(
id_Tasks int primary key identity,
id_Project int not null,
Namee nvarchar(100) not null,
Descriptionn nvarchar(200),
id_Priority int not null,
Start_Datee date not null,
End_Datee date,
id_Status int not null,
Time_Spent nvarchar(25),
id_User int not null,
Foreign key (id_Project) references Production_Project(id_Project),
Foreign key (id_Priority) references Priorityy(id_Priority),
Foreign key (id_Status) references Statuss(id_Status),
Foreign key (id_User) references Userr(id_User),
)

insert Rolee values('Сотрудник'), ('Менеджер'), ('Администратор')

insert Post values ('Разработчик'), ('Тестировщик'), ('Системный аналитик'), ('Менеджер по проектам'), ('Администратор технической поддержки')

insert Userr values ('Developer', 'Developer', 'Developer', 1, 'Employee@mail.ru', '+7(777)353-23-25', 'Employee123', 1),
('Manager', 'Manager', 'Manager', 4, 'Manager@mail.ru', '+7(777)353-23-26', 'Manager123', 2),
('Admin', 'Admin', 'Admin', 5, 'Admin@mail.ru', '+7(777)353-23-27', 'Admin123', 3),
('Панов', 'Данил', 'Олегович', 1, 'Pano@mail.ru', '+7(777)353-23-23', 'Pano123', 1),
('Лаптев', 'Александр', 'Анатольевич', 2, 'Lapka@mail.ru', '+7(999)355-44-11', 'Lapka123', 1),
('Кокорин', 'Дмитрий', 'Сергеевич', 3, 'Korka@mail.ru', '+7(222)333-24-12', 'Korka123', 1),
('Попов', 'Максим', 'Николаевич', 1, 'MaskBigBro@mail.ru', '+7(777)353-23-23', 'Mask1234', 1),
('Иванов', 'Иван', 'Иванович', 2, 'Ivanov@mail.ru', '+7(999)355-44-11', 'Ivanov123', 1),
('Смирнов', 'Николай', 'Александрович', 3, 'Smirnov@mail.ru', '+7(222)333-24-12', 'Smirnov123', 1),
('Кузнецова', 'Мария', 'Викторовна', 1, 'KuznezMaria@mail.ru', '+7(777)353-23-23', 'Kuznezova123', 1),
('Сидоров', 'Алексей', 'Валентинович', 2, 'Sidorov@mail.ru', '+7(999)355-44-11', 'Sidorov123', 1),
('Иванова', 'Светлана', 'Павловна', 3, 'Ivanova@mail.ru', '+7(222)333-24-12', 'Ivanova123', 1),
('Орлова', 'Екатерина', 'Юрьевна', 1, 'Orlova@mail.ru', '+7(222)333-24-12', 'Korka123', 1)

insert Resource_Type values ('Аппаратные ресурсы'), ('Программные ресурсы'),
('Облачные ресурсы'), ('Человеческие ресурсы'),
('Информационные ресурсы')

insert Resourcee values ('1', 'Сервер Dell PowerEdge R750', 'Высокопроизводительный сервер для развертывания виртуальных машин и корпоративных баз данных', 'Ограниченная'),
('2', 'Лицензия Microsoft Visual Studio Enterprise', 'IDE для разработки и отладки ПО, включая инструменты для тестирования и аналитики кода', 'Доступна для разработчиков и тестировщиков по запросу'),
('3', 'Yandex Cloud Compute Instance', 'Виртуальный сервер в облаке для хостинга веб-приложений и тестовых сред', 'Управляется DevOps, выделяется по заявке'),
('4', 'Внешний DevOps-инженер (аутсорс)', 'Специалист по настройке CI/CD и мониторингу инфраструктуры, привлекаемый для сложных задач', 'Временный доступ (по договору)'),
('5', 'Корпоративная Wiki (на базе Confluence)', 'Централизованное хранилище документации, гайдов и регламентов IT-отдела', 'Открыта для всех сотрудников компании с разным уровнем прав')

insert Statuss values ('В процессе'), ('В ожидании'), ('Завершено')

insert Priorityy values ('Низкий'), ('Средний'), ('Высокий'), ('Критичный')

insert Production_Project values ('Оптимизация серверной инфраструктуры', 'Модернизация серверного оборудования для повышения производительности', '2025-04-01', '2024-05-26', 1500000, 1, 1),
('Внедрение системы виртуализации', 'Развертывание VMware vSphere для создания виртуальных сред', '2025-03-10', '2024-06-10', 800000, 1, 2),
('Миграция почтовой системы в облако', 'Перенос корпоративной почты на Microsoft 365', '2025-02-24', '2025-06-05', 450000, 2, 3),
('Разработка внутреннего API', 'Создание унифицированного API для интеграции корпоративных систем', '2024-10-10', '2024-12-30', 950000, 3, 2),
('Организация системы резервного копирования', 'Настройка автоматизированного бэкапа критически важных данных', '2024-11-15', '2024-12-31', 600000, 3, 5)

insert Tasks values (1, 'Анализ нагрузки на API-серверы', 'Сбор метрик производительности текущего API', 3, '2025-04-02', '2025-04-04', 3, '08:00:00', 6),
(2, 'Реализация core-модуля API', 'Разработка базовых классов и методов на C#', 2, '2025-02-25', '2025-04-25', 1, '22:00:00', 4),
(3, 'Тестирование API интеграции с Microsoft 365', 'Проверка работы скриптов миграции', 1, '2025-02-26', '2025-04-30', 2, '21:00:00', 5),
(5, 'Разработка скрипта бэкапа API-конфигураций', 'Создание PowerShell-скрипта для экспорта настроек', 1, '2024-11-12', '2024-11-24', 3, '12:00:00', 10),
(4, 'Рефакторинг кода авторизации', 'Оптимизация модуля JWT-аутентификации', 2, '2024-10-15', '2024-11-20', 3, '15:00:00', 7)

insert Project_Team values (1, 6), (2, 4), (3, 5), (4,7), (5, 10)

insert Eventt values ('2', 'День тестирования в облаке', 'Практикум по работе с Yandex Cloud: развертывание тестовых сред и нагрузочное тестирование',
'2025-06-15', '2025-06-15', 'Удаленно (доступ к облаку)'),
('2', 'Митап «Интеграция 1С и CRM»', 'Разбор кейсов по API-интеграциям для производственных систем',
'2025-05-26', '2025-05-26', 'Конференц-зал (1 этаж)'),
('2', 'Курс по PowerShell для бэкапов', 'Обучение написанию скриптов для автоматизации резервного копирования',
'2025-06-20', '2025-06-22', 'Компьютерный класс IT-отдела'),
('2', 'Тест-драйв нового API', 'Коллективное тестирование методов API с поиском edge-кейсов',
'2025-07-09', '2025-07-09', 'Тестовая среда (Yandex Cloud)'),
('2', 'Документационный спринт', 'Заполнение Корпоративной Wiki: обновление гайдов по API и PowerShell',
'2025-07-29', '2025-07-29', 'Удаленно')

insert News values ('Запуск нового API для интеграции 1С', 'Разработчики завершили работу над внутренним API, который упростит обмен данными между 1С и CRM. Тестирование начнется 25.06.2025.',
'2025-05-01', '2'),
('Переход на Microsoft 365', 'Миграция почтовой системы в облако завершится 15.10.2024. Все сотрудники получат инструкции в Корпоративной Wiki',
'2025-04-28', '2'),
('Тестовые виртуальные машины готовы к работе', 'В Oracle VirtualBox развернуты 5 тестовых ВМ для разработчиков',
'2025-05-15', '2'),
('PowerShell-скрипты для бэкапа', 'Готовы скрипты для резервного копирования конфигураций API. Доступны в GitHub.',
'2025-04-24', '2'),
('Результаты тестов в облаке', 'API успешно протестирован в Yandex Cloud. Отчет — в базе знаний',
'2024-10-11', '2')

insert Documentation values ('1', '1', '60', '20000', '0'),
('1', '2', '80', '15600', '0'),
('2', '3', '47', '9800', '0'),
('3', '4', '100', '45000', '88000'),
('3', '5', '100', '20000', '100000')