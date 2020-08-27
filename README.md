<!--
*** Thanks for checking out this README Template. If you have a suggestion that would
*** make this better, please fork the MemberAnalyzer and create a pull request or simply open
*** an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
***
***
***
*** To avoid retyping too much info. Do a search and replace for the following:
*** Yuan-Quan, MemberAnalyzer, twitter_handle, metroyuan@gmail.com
-->





<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
<!--[![LinkedIn][linkedin-shield]][linkedin-url]-->



<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/Yuan-Quan/MemberAnalyzer">
    <img src="images/LOGO.jpg" alt="Logo" width="160" height="160">
  </a>

  <h3 align="center">MEMBER ANALYZER</h3>

  <p align="center">
    昆明八中音乐社用 提取qq群成员信息. 分析群名片以分类群成员
    <br />
    <a href="https://github.com/Yuan-Quan/MemberAnalyzer"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Yuan-Quan/MemberAnalyzer">View Demo</a>
    ·
    <a href="https://github.com/Yuan-Quan/MemberAnalyzer/issues">Report Bug</a>
    ·
    <a href="https://github.com/Yuan-Quan/MemberAnalyzer/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Usage](#usage)
* [Roadmap](#roadmap)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://example.com)

**音乐社人太多了, 手动统计社员名单会死的**

### Built With

* [CommandDotnet](https://github.com/bilal-fazlani/commanddotnet/)
* [ConsoleTables](https://github.com/khalidabuhakmeh/ConsoleTables)
* [.NetCore 3.1](https://github.com/dotnet/core)



<!-- GETTING STARTED -->
## Getting Started

获得一份此程序的副本, 并在自己的设备上运行起来.

### Prerequisites

*需要先安装.NetCore 3.1的运行环境
  
__[下载运行环境](https://dotnet.microsoft.com/download)__
  
下载完了吗? 安装好了吗? 我等你...
  
*需要一个Console来进行指令行下的操作
选一个自己喜欢的就行, 什么powershell, gitbash都可以

### Installation Guide for Windows
什么? 你想用Linux, 自己编译一下吧
  
[下载最新版本的本程序](https://github.com/Yuan-Quan/MemberAnalyzer/releases)
解压到你喜欢的目录.
为使得在任何目录都可以使用, 请把本文件夹的地址[加入PATH环境变量](https://www.architectryan.com/2018/03/17/add-to-the-path-on-windows-10/)
 
安装成功之后, 在你的终端中键入 
```bash 
$ MemberAnalyzer.exe 
``` 
应该会出现帮助的内容:
```bash
Usage: MemberAnalyzer.exe [command] [options]

Options:

  -v | --version
  Show version information

Commands:

  config       view change/add settings
  deserialize  Deserialize text copid from QQ web, then serialize them into a xml file

Use "MemberAnalyzer.exe [command] --help" for more information about a command.
```
如果没有, 就是整错了

## Usage
登录QQ群官网的成员管理页面  
用这个地址
```
https://qun.qq.com/member.html#gid=
```
gid=后面写群号

![eg1](/images/eg1.png)

像这样把全部人都选中, 然后复制粘贴到一个新建的文本文档里面
![eg2](/images/eg2.png)
暂且把它叫做New Text Document.txt

打开终端, 使用本程序的 ```deserialize -f [path]``` 命令以解析这个文件  
可选: 传入-s参数可以自定义xml文件保存位置, 否则将存在当前目录
![eg3](/images/eg3.png)
如图, 成功生成解析后的xml文件
  
它看起来大概是这样的
![eg4](/images/eg4.png)
<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/Yuan-Quan/MemberAnalyzer/issues) for a list of proposed features (and known issues).



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the General Public License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

袁泉 - [@twitter_handle](https://twitter.com/twitter_handle) - metroyuan@gmail.com

Project Link: [https://github.com/Yuan-Quan/MemberAnalyzer](https://github.com/Yuan-Quan/MemberAnalyzer)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [额]()
* [没了]()
* [你走吧]()





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Yuan-Quan/MemberAnalyzer.svg?style=flat-square
[contributors-url]: https://github.com/Yuan-Quan/MemberAnalyzer/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Yuan-Quan/MemberAnalyzer.svg?style=flat-square
[forks-url]: https://github.com/Yuan-Quan/MemberAnalyzer/network/members
[stars-shield]: https://img.shields.io/github/stars/Yuan-Quan/MemberAnalyzer.svg?style=flat-square
[stars-url]: https://github.com/Yuan-Quan/MemberAnalyzer/stargazers
[issues-shield]: https://img.shields.io/github/issues/Yuan-Quan/MemberAnalyzer.svg?style=flat-square
[issues-url]: https://github.com/Yuan-Quan/MemberAnalyzer/issues
[license-shield]: https://img.shields.io/github/license/Yuan-Quan/MemberAnalyzer.svg?style=flat-square
[license-url]: https://github.com/Yuan-Quan/MemberAnalyzer/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/Yuan-Quan
[product-screenshot]: images/screenshot.png
