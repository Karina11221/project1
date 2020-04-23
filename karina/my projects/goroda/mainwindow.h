#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "ui_mainwindow.h"

class MainWindow : public QMainWindow, private Ui::MainWindow
{
    Q_OBJECT

    QVector <QString> words;

public:
    int n,sum;
    explicit MainWindow(QWidget *parent = 0);
private slots:
    void on_pushButton_clicked();
};

#endif // MAINWINDOW_H
