# WordHiddenPowers

Проект состоит из следующих компонентов:

1. `WordHiddenPowers\WordHiddenPowers\Const\`

	константы;

2. `WordHiddenPowers\WordHiddenPowers\Controls\`

	элементы управления;

	Являются имплементацией библиотеки  [ControlLibrary](https://github.com/Kazannik/ControlLibrary.git).

3. `WordHiddenPowers\WordHiddenPowers\Dialogs\`
 
	диалоговые окна;

4. `WordHiddenPowers\WordHiddenPowers\Documents\`

	слой работы с документами.
	
	Представляет из себя обертку для `Microsoft.Office.Interop.Word.Document`

5. `WordHiddenPowers\WordHiddenPowers\Files\`

	папка для резмещения временных файлов (будет удалена)	

6.	`WordHiddenPowers\WordHiddenPowers\hooks\`

	слой для перехвата взаимодействия пользователя с документами MS Word по средством клавиатуры и мыши;

7. `WordHiddenPowers\WordHiddenPowers\LLMService\`

	Слой для реализации асинхронного взаимодействия с LLM; 

8.	`WordHiddenPowers\WordHiddenPowers\MLService\`

	Слой для реализации взаимодействия с нейросетями, обученными с помощью фреймворка ML.NET 

9.	`WordHiddenPowers\WordHiddenPowers\Panes\`

	слой для реализации боковых панелей в MS Word

10. `WordHiddenPowers\WordHiddenPowers\Repository\`
 
	слой управления данными. На этом уровне формируются данные в виде `DataSet`, которые конвертируются в формат `XML` и сохраняются в строковых внутренних переменных документа MS Word (`Microsoft.Office.Interop.Word.Document`);

11. `WordHiddenPowers\Resources\`
	
	размещение всех ресурсов за исключением файлов `JSON`, которые хранятся непосредственно рядов с классом, который их использует.  

12. `WordHiddenPowers\WordHiddenPowers\Services\`

	слой сервисов; реализация бизнес-логики отдельных процессов.

13. `WordHiddenPowers\WordHiddenPowers\Utils\`

---

`WordHiddenPowers\WordHiddenPowers\MLModel.filters` - файл словаря. Содержит слова без окончаний, использован для обучении отдельных нейросетей.