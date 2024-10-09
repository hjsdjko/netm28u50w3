<template>
  <div :style='{"padding":"30px","margin":"0"}'>
    <el-form
	  :style='{"border":"1px solid rgba(167, 180, 201,.3)  ","padding":"30px","borderRadius":"6px","background":"#fff"}'
      class="add-update-preview"
      ref="ruleForm"
      :rules="rules"
      :model="ruleForm"
      label-width="80px"
    >
      <el-form-item :style='{"margin":"0 0 20px 0"}' label="原密码" prop="password">
        <el-input v-model="ruleForm.password" show-password></el-input>
      </el-form-item>
      <el-form-item :style='{"margin":"0 0 20px 0"}' label="新密码" prop="newpassword">
        <el-input v-model="ruleForm.newpassword" show-password></el-input>
      </el-form-item>
      <el-form-item :style='{"margin":"0 0 20px 0"}' label="确认密码" prop="repassword">
        <el-input v-model="ruleForm.repassword" show-password></el-input>
      </el-form-item>
      <el-form-item :style='{"padding":"0","margin":"0"}'>
		<el-button class="btn3" :style='{"border":"0","cursor":"pointer","padding":"0 24px","margin":"4px","outline":"none","color":"#fff","borderRadius":"4px","background":"#ff2b88","width":"auto","fontSize":"14px","height":"40px"}' type="primary" @click="onUpdateHandler">
			<span class="icon iconfont icon-tijiao20" :style='{"margin":"0 2px","fontSize":"14px","color":"#fff","height":"40px"}'></span>
			提交
		</el-button>
	  </el-form-item>
    </el-form>
  </div>
</template>
<script>
export default {
	data() {
		return {
			dialogVisible: false,
			ruleForm: {},
			user: {},
			rules: {
				password: [
					{
						required: true,
						message: "密码不能为空",
						trigger: "blur"
					}
				],
				newpassword: [
					{
						required: true,
						message: "新密码不能为空",
						trigger: "blur"
					}
				],
				repassword: [
					{
						required: true,
						message: "确认密码不能为空",
						trigger: "blur"
					}
				]
			}
		};
	},
	mounted() {
		this.$http({
			url: `${this.$storage.get("sessionTable")}/session`,
			method: "get"
		}).then(({ data }) => {
			if (data && data.code === 0) {
				this.user = data.data;
			} else {
				this.$message.error(data.msg);
			}
		});
	},
	methods: {
		onLogout() {
			this.$storage.remove("Token");
			this.$router.replace({ name: "login" });
		},
		// 修改密码
		async onUpdateHandler() {
			this.$refs["ruleForm"].validate(async valid => {
				if (valid) {
					var password = "";
					if (this.user.mima) {
						password = this.user.mima;
					} else if (this.user.password) {
						password = this.user.password;
					}
					if(this.$storage.get("sessionTable")=='users'){
						if (this.ruleForm.password != password) {
							this.$message.error("原密码错误");
							return;
						}
						if (this.ruleForm.newpassword != this.ruleForm.repassword) {
							this.$message.error("两次密码输入不一致");
							return;
						}
						this.user.password = this.ruleForm.newpassword;
						this.user.mima = this.ruleForm.newpassword;
						this.$http({
							url: `${this.$storage.get("sessionTable")}/update`,
							method: "post",
							data: this.user
						}).then(({ data }) => {
							if (data && data.code === 0) {
								this.$message({
									message: "修改密码成功,下次登录系统生效",
									type: "success",
									duration: 1500,
									onClose: () => {
									}
								});
							} else {
								this.$message.error(data.msg);
							}
						});
						return false
					}
					if (this.ruleForm.password != password) {
						this.$message.error("原密码错误");
						return;
					}
					if (this.ruleForm.newpassword != this.ruleForm.repassword) {
						this.$message.error("两次密码输入不一致");
						return;
					}
					if (this.ruleForm.newpassword == this.ruleForm.password) {
						this.$message.error("新密码与原密码相同");
						return;
					}
					if (this.$storage.get("sessionTable") == 'caigouyuan') {
					}
					if (this.$storage.get("sessionTable") == 'xiaoshouyuan') {
					}
					this.user.password = this.ruleForm.newpassword;
					this.user.mima = this.ruleForm.newpassword;
					this.$http({
						url: `${this.$storage.get("sessionTable")}/update`,
						method: "post",
						data: this.user
					}).then(({ data }) => {
						if (data && data.code === 0) {
							this.$message({
								message: "修改密码成功,下次登录系统生效",
								type: "success",
								duration: 1500,
								onClose: () => {
								}
							});
						} else {
							this.$message.error(data.msg);
						}
					});
				}
			});
		}
	}
};
</script>
<style lang="scss" scoped>
	.el-date-editor.el-input {
		width: auto;
	}
	
	.add-update-preview .el-form-item /deep/ .el-form-item__label {
	  	  padding: 0 10px 0 0;
	  	  color: #374254;
	  	  font-weight: 600;
	  	  width: 80px;
	  	  font-size: 14px;
	  	  line-height: 40px;
	  	  text-align: right;
	  	}
	
	.add-update-preview .el-form-item /deep/ .el-form-item__content {
	  margin-left: 80px;
	}
	
	.add-update-preview .el-input /deep/ .el-input__inner {
	  	  border: 1px solid rgba(167, 180, 201,.3);
	  	  border-radius: 4px;
	  	  padding: 0 12px;
	  	  outline: none;
	  	  color: #a7b4c9;
	  	  width: 400px;
	  	  font-size: 14px;
	  	  height: 40px;
	  	}
	
	.add-update-preview .el-select /deep/ .el-input__inner {
	  	  border:  1px solid rgba(167, 180, 201,.3) ;
	  	  border-radius: 4px;
	  	  padding: 0 10px;
	  	  outline: none;
	  	  color: #a7b4c9;
	  	  width: 400px;
	  	  font-size: 14px;
	  	  height: 40px;
	  	}
	
	.add-update-preview .el-date-editor /deep/ .el-input__inner {
	  	  border:  1px solid rgba(167, 180, 201,.3) ;
	  	  border-radius: 4px;
	  	  padding: 0 10px 0 30px;
	  	  outline: none;
	  	  color: #a7b4c9;
	  	  width: 400px;
	  	  font-size: 14px;
	  	  height: 40px;
	  	}
	
	.add-update-preview /deep/ .el-upload--picture-card {
		background: transparent;
		border: 0;
		border-radius: 0;
		width: auto;
		height: auto;
		line-height: initial;
		vertical-align: middle;
	}
	
	.add-update-preview /deep/ .el-upload-list .el-upload-list__item {
	  	  border: 2px dashed rgba(167, 180, 201,.3) ;
	  	  cursor: pointer;
	  	  border-radius: 6px;
	  	  color: #a7b4c9  ;
	  	  width: 200px;
	  	  font-size: 32px;
	  	  line-height: 200px;
	  	  text-align: center;
	  	  height: 200px;
	  	}
	
	.add-update-preview /deep/ .el-upload .el-icon-plus {
	  	  border: 2px dashed rgba(167, 180, 201,.3) ;
	  	  cursor: pointer;
	  	  border-radius: 6px;
	  	  color: #a7b4c9  ;
	  	  width: 200px;
	  	  font-size: 32px;
	  	  line-height: 200px;
	  	  text-align: center;
	  	  height: 200px;
	  	}
	
	.add-update-preview .el-textarea /deep/ .el-textarea__inner {
	  	  border: 1px solid rgba(167, 180, 201,.3)     ;
	  	  border-radius: 4px;
	  	  padding: 12px;
	  	  outline: none;
	  	  color: #a7b4c9 ;
	  	  width: 400px;
	  	  font-size: 14px;
	  	  height: 120px;
	  	}
	
	.add-update-preview .btn3 {
				border: 0;
				cursor: pointer;
				border-radius: 4px;
				padding: 0 24px;
				margin: 4px;
				outline: none;
				color: #fff;
				background: #ff2b88;
				width: auto;
				font-size: 14px;
				height: 40px;
			}
	
	.add-update-preview .btn3:hover {
				opacity: 0.8;
			}
</style>
