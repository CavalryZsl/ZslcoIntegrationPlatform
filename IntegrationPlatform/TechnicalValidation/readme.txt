ASP.NET CORE 基础知识
StartUp配置
Controller
Razor
Model Validation
前端
Entity Framework core
授权和身份验证 asp.net core identity

安全
测试
性能
日志
CI/CD

管道和中间件
中间件:决定如何处理请求 其实就是一个对象 各个中间件关注各自的功能,
		例如Logger日志中间件,记录相关信息,如果没问题 然后转入下一个中间件 否组原路返回响应
		授权中间件处理 如果没问题 然后转入下一个中间件 否组原路返回响应
		路由中间件处理 如果没问题 然后转入下一个中间件 否组原路返回响应
		......
		MVC也是一个中间件
		按照添加中间件的顺序 依次执行

路由:
	约定路由
	特性标签路由

	VIEW partial view
	ViewComponents 可复用  独立的  不依赖父级的数据  相当于迷你的MVC