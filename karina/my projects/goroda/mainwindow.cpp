#include "mainwindow.h"
#include "QMessageBox"
#include <iostream>
#include <QApplication>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent)
{
    setupUi(this);

void MainWindow::on_pushButton_clicked()
{


    QString str=lineEdit->text();
    textEdit->insertPlainText(lineEdit->text()+"\n");
     while (!n=0)
        {
           sum+=n%10;
           n/= 10;



         }
     cout << "sum = " << sum <<endl;


}
    lineEdit->clear();
}
