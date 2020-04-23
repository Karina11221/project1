#include "mainwindow.h"
#include "QMessageBox"
#include <iostream>
#include <QApplication>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent)
{
    setupUi(this);
    words <<"Кот"<<"Рак"<<"Азбука"<<"Слон"<<"Небо"<<"Обруч"<<"Человек"<<"Красота"<<"Автопилот"<<"Трон"<<"Нос"<<"Собака"<<"Адрес"<<"Сирота"<<"Аквариум"<<"Морж"<<"Жар"<<"Работа"<<"Аккорд"<<"Дерево";

}
void MainWindow::on_pushButton_clicked()
{




    QString str=lineEdit->text();
    textEdit->insertPlainText(lineEdit->text()+"\n");
    for (int i=0;i<words.count();i++)
    {
        if (lineEdit->text().right(1).toLower()==words[i].left(1).toLower())
        {
            textEdit_2->insertPlainText(words[i]+"\n");
            words[i]='*';

            break;
        }

}
    lineEdit->clear();
}
