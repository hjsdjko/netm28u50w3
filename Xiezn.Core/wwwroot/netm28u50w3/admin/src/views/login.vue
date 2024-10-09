<template>
  <div>
    <div class="container" :style='{"minHeight":"100vh","padding":"100px  100px  0 0","alignItems":"center","background":"url(http://codegen.caihongy.cn/20240330/83e54fd0c14b48a6a366638d7e19a66d.jpg)","display":"flex","width":"100%","backgroundSize":"100% 100%","backgroundPosition":"center top","backgroundRepeat":"no-repeat","justifyContent":"flex-end"}'>
      <el-form :style='{"padding":"40px 20px 20px","boxShadow":"0 1px 20px rgba( 255,  255, 255, .8)","margin":"0","borderRadius":"4px","background":"#fff","width":"500px","height":"auto"}'>
        <div v-if="true" :style='{"width":"100%","margin":"0 0 10px 0","lineHeight":"44px","fontSize":"20px","color":"#374254","textAlign":"center"}' class="title-container">小型超市商品管理系统登录</div>
        <div v-if="loginType==1" class="list-item" :style='{"width":"80%","margin":"0 auto 10px","alignItems":"center","flexWrap":"wrap","display":"flex"}'>
          <div v-if="true" class="lable" :style='{"width":"64px","lineHeight":"44px","fontSize":"14px","color":" #374254","fontWeight":"600"}'>用户名：</div>
          <input :style='{"border":"1px solid rgba(167, 180, 201,.3) ","padding":"0 10px","color":" #a7b4c9 ","outlineOffset":"4px","width":"100%","fontSize":"14px","height":"44px"}' placeholder="请输入用户名" name="username" type="text" v-model="rulesForm.username">
        </div>
        <div v-if="loginType==1" class="list-item" :style='{"width":"80%","margin":"0 auto 10px","alignItems":"center","flexWrap":"wrap","display":"flex"}'>
          <div v-if="true" class="lable" :style='{"width":"64px","lineHeight":"44px","fontSize":"14px","color":" #374254","fontWeight":"600"}'>密码：</div>
          <input :style='{"border":"1px solid rgba(167, 180, 201,.3) ","padding":"0 10px","color":" #a7b4c9 ","outlineOffset":"4px","width":"100%","fontSize":"14px","height":"44px"}' placeholder="请输入密码" name="password" type="password" v-model="rulesForm.password">
        </div>

        <div :style='{"width":"80%","margin":"20px auto"}' v-if="roles.length>1" prop="loginInRole" class="list-type">
          <el-radio v-if="loginType==1||(loginType==2&&item.roleName!='管理员')" v-for="item in roles" v-bind:key="item.roleName" v-model="rulesForm.role" :label="item.roleName">{{item.roleName}}</el-radio>
        </div>

		
        <div :style='{"width":"80%","margin":"20px auto","alignItems":"center","flexWrap":"wrap","display":"flex"}'>
          <el-button v-if="loginType==1" :style='{"border":"0","cursor":"pointer","padding":"0 24px","margin":"10px","outline":"none","color":"#fff","borderRadius":"4px","background":"rgb(45, 220, 211)","width":"auto","fontSize":"14px","height":"44px"}' type="primary" @click="login()" class="loginInBt">登录</el-button>
          <el-button :style='{"cursor":"pointer","border":"0","padding":"0 24px","margin":"10px","outline":"none","color":"#fff","borderRadius":"4px","background":"rgb(15, 116, 253)","width":"auto","fontSize":"14px","height":"44px"}' type="primary" @click="register('caigouyuan')" class="register">注册采购员</el-button>
          <el-button :style='{"cursor":"pointer","border":"0","padding":"0 24px","margin":"10px","outline":"none","color":"#fff","borderRadius":"4px","background":"rgb(15, 116, 253)","width":"auto","fontSize":"14px","height":"44px"}' type="primary" @click="register('xiaoshouyuan')" class="register">注册销售员</el-button>
        </div>
      </el-form>

    </div>
  </div>
</template>
<script>
import menu from "@/utils/menu";
export default {
  data() {
    return {
		verifyCheck2: false,
		flag: false,
      baseUrl:this.$base.url,
      loginType: 1,
      rulesForm: {
        username: "",
        password: "",
        role: "",
      },
      menus: [],
      roles: [],
      tableName: "",
    };
  },
  mounted() {
    let menus = menu.list();
    this.menus = menus;

    for (let i = 0; i < this.menus.length; i++) {
      if (this.menus[i].hasBackLogin=='是') {
        this.roles.push(this.menus[i])
      }
    }

  },
  created() {

  },
  destroyed() {
	    },
  components: {
  },
  methods: {

    //注册
    register(tableName){
		this.$storage.set("loginTable", tableName);
		this.$router.push({path:'/register',query:{pageFlag:'register'}})
    },
    // 登陆
    login() {

		if (!this.rulesForm.username) {
			this.$message.error("请输入用户名");
			return;
		}
		if (!this.rulesForm.password) {
			this.$message.error("请输入密码");
			return;
		}
		if(this.roles.length>1) {
			if (!this.rulesForm.role) {
				this.$message.error("请选择角色");
				return;
			}

			let menus = this.menus;
			for (let i = 0; i < menus.length; i++) {
				if (menus[i].roleName == this.rulesForm.role) {
					this.tableName = menus[i].tableName;
				}
			}
		} else {
			this.tableName = this.roles[0].tableName;
			this.rulesForm.role = this.roles[0].roleName;
		}
		
		this.loginPost()
    },
	loginPost() {
		this.$http({
			url: `${this.tableName}/login?username=${this.rulesForm.username}&password=${this.rulesForm.password}`,
			method: "post"
		}).then(({ data }) => {
			if (data && data.code === 0) {
				this.$storage.set("Token", data.token);
				this.$storage.set("role", this.rulesForm.role);
				this.$storage.set("sessionTable", this.tableName);
				this.$storage.set("adminName", this.rulesForm.username);
				this.$router.replace({ path: "/" });
			} else {
				this.$message.error(data.msg);
			}
		});
	},
  }
}
</script>

<style lang="scss" scoped>
.container {
  min-height: 100vh;
  position: relative;
  background-repeat: no-repeat;
  background-position: center center;
  background-size: cover;
      background: url(http://codegen.caihongy.cn/20240330/83e54fd0c14b48a6a366638d7e19a66d.jpg);
        
  .list-item /deep/ .el-input .el-input__inner {
		border: 1px solid rgba(167, 180, 201,.3) ;
		padding: 0 10px;
		color:  #a7b4c9 ;
		width: 100%;
		font-size: 14px;
		outline-offset: 4px;
		height: 44px;
	  }
  
  .list-item.select /deep/ .el-select .el-input__inner {
		border: 1px solid rgba(64, 158, 255, 1);
		padding: 0 10px;
		box-shadow: 0 0 6px rgba(64, 158, 255, .5);
		outline: 1px solid #efefef;
		color: rgba(64, 158, 255, 1);
		width: 288px;
		font-size: 14px;
		outline-offset: 4px;
		height: 44px;
	  }
  
  .list-code /deep/ .el-input .el-input__inner {
  	  	border: 1px solid rgba(167, 180, 201,.3) ;
  	  	padding: 0 10px;
  	  	outline: none;
  	  	color:  #a7b4c9 ;
  	  	width: calc(100% - 80px);
  	  	font-size: 14px;
  	  	height: 44px;
  	  }

  .list-type /deep/ .el-radio__input .el-radio__inner {
		background: rgba(53, 53, 53, 0);
		border-color: #666666;
	  }
  .list-type /deep/ .el-radio__input.is-checked .el-radio__inner {
        background: #3b5998;
        border-color: #3b5998;
      }
  .list-type /deep/ .el-radio__label {
		color: #666666;
		font-size: 14px;
	  }
  .list-type /deep/ .el-radio__input.is-checked+.el-radio__label {
        color: #3b5998;
        font-size: 14px;
      }
}

</style>
