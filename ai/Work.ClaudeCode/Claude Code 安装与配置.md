# Claude Code 安装与配置

### 安装 Claude Code

安装 Claude Code之前，需要先安装如下几个软件：

- Node.js，检测方式：

  ```
  node --version
  npm --version
  ```

- Git

安装完成之后，可以直接通过npm进行Claude Code的安装。

注意：npm默认是国外的仓库，可以使用如下方式，配置成国内淘宝镜像：

```
npm config set registry https://registry.npmmirror.com/
```

然后输入下述命令安装Claude Code：

```
npm install -g @anthropic-ai/claude-code
```

执行完毕之后，输入下述命令进行验证：

```
claude --version
```

如果能够输出版本号，即安装完成。

### 配置代理网络和  Claude API Key

#### 配置代理网络

假如不使用第三方中转站的情况下，直接无法访问Claude API，需要配置代理才可以。

在项目目录下创建 `.claude/settings.json` 文件：

```json
{
    "env":{
        "HTTP_PROXY":"http://127.0.0.1:7897",
        "HTTPS_PROXY":"http://127.0.0.1:7897"
    }
}
```

#### 配置 API Key 环境变量

推荐使用CC-Switch来进行配置。

### 安装 CC-Switch

下载地址：[Releases · farion1231/cc-switch](https://github.com/farion1231/cc-switch/releases)

配置第三方中转站提供的API Key即可。

由于使用了第三方中转站，因此不需要配置代理即可进行对话。





