<template>
	<div>
		<div class="container" :style='{"minHeight":"100vh","padding":"20px","alignItems":"center","background":"url(http://codegen.caihongy.cn/20240330/83e54fd0c14b48a6a366638d7e19a66d.jpg)","display":"flex","width":"auto","backgroundSize":"100% 100%","backgroundPosition":"center top","backgroundRepeat":"no-repeat","justifyContent":"flex-end"}'>
			<el-form v-if="pageFlag=='register'" :style='{"boxShadow":"0 1px 20px rgba( 255,  255, 255, .8)","padding":"20px 0","margin":"0px 90px 0px 0px","borderRadius":"4px","background":"#fff","width":"500px","height":"auto"}' ref="rgsForm" class="rgs-form" :model="rgsForm" :rules="rules">
				<div v-if="true" :style='{"width":"100%","margin":"0 0 10px 0","lineHeight":"44px","fontSize":"20px","color":"#374254","textAlign":"center"}' class="title">小型超市商品管理系统注册</div>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('caigougonghao')?'required':''">采购工号：</div>
					<el-input  v-model="ruleForm.caigougonghao"  autocomplete="off" placeholder="采购工号"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('caigouxingming')?'required':''">采购姓名：</div>
					<el-input  v-model="ruleForm.caigouxingming"  autocomplete="off" placeholder="采购姓名"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('mima')?'required':''">密码：</div>
					<el-input  v-model="ruleForm.mima"  autocomplete="off" placeholder="密码"  type="password"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('mima')?'required':''">确认密码：</div>
					<el-input  v-model="ruleForm.mima2" autocomplete="off" placeholder="确认密码" type="password" />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('xingbie')?'required':''">性别：</div>
                    <el-select v-model="ruleForm.xingbie" placeholder="请选择性别" >
                        <el-option
                            v-for="(item,index) in caigouyuanxingbieOptions"
                            v-bind:key="index"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('zhicheng')?'required':''">职称：</div>
					<el-input  v-model="ruleForm.zhicheng"  autocomplete="off" placeholder="职称"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('lianxidianhua')?'required':''">联系电话：</div>
					<el-input  v-model="ruleForm.lianxidianhua"  autocomplete="off" placeholder="联系电话"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='caigouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('touxiang')?'required':''">头像：</div>
                    <file-upload
                        tip="点击上传头像"
                        action="file/upload"
                        :limit="3"
                        :multiple="true"
                        :fileUrls="ruleForm.touxiang?ruleForm.touxiang:''"
                        @change="caigouyuantouxiangUploadChange"
                    ></file-upload>
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('yuangonggonghao')?'required':''">员工工号：</div>
					<el-input  v-model="ruleForm.yuangonggonghao"  autocomplete="off" placeholder="员工工号"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('yuangongxingming')?'required':''">员工姓名：</div>
					<el-input  v-model="ruleForm.yuangongxingming"  autocomplete="off" placeholder="员工姓名"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('lianxidianhua')?'required':''">联系电话：</div>
					<el-input  v-model="ruleForm.lianxidianhua"  autocomplete="off" placeholder="联系电话"  type="text"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('mima')?'required':''">密码：</div>
					<el-input  v-model="ruleForm.mima"  autocomplete="off" placeholder="密码"  type="password"  />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('mima')?'required':''">确认密码：</div>
					<el-input  v-model="ruleForm.mima2" autocomplete="off" placeholder="确认密码" type="password" />
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('xingbie')?'required':''">性别：</div>
                    <el-select v-model="ruleForm.xingbie" placeholder="请选择性别" >
                        <el-option
                            v-for="(item,index) in xiaoshouyuanxingbieOptions"
                            v-bind:key="index"
                            :label="item"
                            :value="item">
                        </el-option>
                    </el-select>
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('touxiang')?'required':''">头像：</div>
                    <file-upload
                        tip="点击上传头像"
                        action="file/upload"
                        :limit="3"
                        :multiple="true"
                        :fileUrls="ruleForm.touxiang?ruleForm.touxiang:''"
                        @change="xiaoshouyuantouxiangUploadChange"
                    ></file-upload>
				</el-form-item>
				<el-form-item :style='{"width":"80%","padding":"0","margin":"0 auto 0","height":"auto"}' class="list-item" v-if="tableName=='xiaoshouyuan'">
					<div v-if="true" :style='{"width":"100%","lineHeight":"44px","fontSize":"14px","position":"relative","color":" #374254"}' class="lable" :class="changeRules('jiatingzhuzhi')?'required':''">家庭住址：</div>
					<el-input  v-model="ruleForm.jiatingzhuzhi"  autocomplete="off" placeholder="家庭住址"  type="text"  />
				</el-form-item>
				<button :style='{"border":"0","cursor":"pointer","padding":"0 10px","margin":"20px auto 5px","outline":"none","color":"#fff","borderRadius":"4px","background":"#dc4e41","display":"block","width":"80%","fontSize":"16px","height":"44px"}' type="button" class="r-btn" @click="login()">注册</button>
				<div :style='{"cursor":"pointer","padding":"0 10%","color":"rgba(159, 159, 159, 1)","display":"inline-block","lineHeight":"1","fontSize":"12px","textDecoration":"underline"}' class="r-login" @click="close()">已有账号，直接登录</div>
			</el-form>
			
		</div>
	</div>
</template>

<script>
export default {
	data() {
		return {
			ruleForm: {
			},
			forgetForm: {},
            pageFlag : '',
			tableName:"",
			rules: {},
            caigouyuanxingbieOptions: [],
            xiaoshouyuanxingbieOptions: [],
		};
	},
	mounted(){
		this.pageFlag = this.$route.query.pageFlag
		if(this.$route.query.pageFlag=='register'){
			
			let table = this.$storage.get("loginTable");
			this.tableName = table;
			if(this.tableName=='caigouyuan'){
				this.ruleForm = {
					caigougonghao: '',
					caigouxingming: '',
					mima: '',
					xingbie: '',
					zhicheng: '',
					lianxidianhua: '',
					touxiang: '',
				}
			}
			if(this.tableName=='xiaoshouyuan'){
				this.ruleForm = {
					yuangonggonghao: '',
					yuangongxingming: '',
					lianxidianhua: '',
					mima: '',
					xingbie: '',
					touxiang: '',
					jiatingzhuzhi: '',
				}
			}
			this.caigouyuanxingbieOptions = "男,女".split(',')
			this.xiaoshouyuanxingbieOptions = "男,女".split(',')
		}
	},
	created() {
	},
	destroyed() {
		  	},
	methods: {
		changeRules(name){
			if(this.rules[name]){
				return true
			}
			return false
		},
		// 获取uuid
		getUUID () {
			return new Date().getTime();
		},
		close(){
			this.$router.push({ path: "/login" });
		},
        caigouyuantouxiangUploadChange(fileUrls) {
            this.ruleForm.touxiang = fileUrls;
        },
        xiaoshouyuantouxiangUploadChange(fileUrls) {
            this.ruleForm.touxiang = fileUrls;
        },

        // 多级联动参数


		// 注册
		login() {
			var url=this.tableName+"/register";
					if((this.ruleForm.mima!=this.ruleForm.mima2) && `caigouyuan` == this.tableName){
						this.$message.error(`两次密码输入不一致`);
						return
					}
					if(`caigouyuan` == this.tableName && this.ruleForm.lianxidianhua &&(!this.$validate.isMobile(this.ruleForm.lianxidianhua))){
						this.$message.error(`联系电话应输入手机格式`);
						return
					}
            if(this.ruleForm.touxiang!=null) {
                this.ruleForm.touxiang = this.ruleForm.touxiang.replace(new RegExp(this.$base.url,"g"),"");
            }
					if(`xiaoshouyuan` == this.tableName && this.ruleForm.lianxidianhua &&(!this.$validate.isMobile(this.ruleForm.lianxidianhua))){
						this.$message.error(`联系电话应输入手机格式`);
						return
					}
					if((this.ruleForm.mima!=this.ruleForm.mima2) && `xiaoshouyuan` == this.tableName){
						this.$message.error(`两次密码输入不一致`);
						return
					}
            if(this.ruleForm.touxiang!=null) {
                this.ruleForm.touxiang = this.ruleForm.touxiang.replace(new RegExp(this.$base.url,"g"),"");
            }
			this.$http({
				url: url,
				method: "post",
				data:this.ruleForm
			}).then(({ data }) => {
				if (data && data.code === 0) {
					this.$message({
						message: "注册成功",
						type: "success",
						duration: 1500,
						onClose: () => {
							this.$router.replace({ path: "/login" });
						}
					});
				} else {
					this.$message.error(data.msg);
				}
			});
		}
	}
};
</script>

<style lang="scss" scoped>
	.container {
	  position: relative;
	  background: url(http://codegen.caihongy.cn/20240330/83e54fd0c14b48a6a366638d7e19a66d.jpg);

		.el-date-editor.el-input {
		  width: 100%;
		}
		
		.rgs-form .el-input /deep/ .el-input__inner {
						border: 1px solid rgba(167, 180, 201,.3)    ;
						border-radius: 0;
						padding: 0 10px;
						outline: none;
						color: #a7b4c9 ;
						width: 100%;
						font-size: 14px;
						height: 40px;
					}
		
		.rgs-form .el-select /deep/ .el-input__inner {
						border: 1px solid rgba(167, 180, 201,.3)    ;
						border-radius: 0;
						padding: 0 10px;
						outline: none;
						color: #a7b4c9 ;
						width: 288px;
						font-size: 14px;
						height: 40px;
					}
		
		.rgs-form .el-date-editor /deep/ .el-input__inner {
						border: 1px solid rgba(167, 180, 201,.3)    ;
						border-radius: 0;
						padding: 0 10px 0 30px;
						outline: none;
						color: #a7b4c9 ;
						width: 288px;
						font-size: 14px;
						height: 40px;
					}
		
		.rgs-form .el-date-editor /deep/ .el-input__inner {
						border: 1px solid rgba(167, 180, 201,.3)    ;
						border-radius: 0;
						padding: 0 10px 0 30px;
						outline: none;
						color: #a7b4c9 ;
						width: 288px;
						font-size: 14px;
						height: 40px;
					}
		
		.rgs-form /deep/ .el-upload--picture-card {
			background: transparent;
			border: 0;
			border-radius: 0;
			width: auto;
			height: auto;
			line-height: initial;
			vertical-align: middle;
		}
		
		.rgs-form /deep/ .upload .upload-img {
		  		  border: 1px dashed  rgba(167, 180, 201,.3)    ;
		  		  cursor: pointer;
		  		  border-radius: 8px;
		  		  color: rgba(167, 180, 201,.3);
		  		  width: 90px;
		  		  font-size: 32px;
		  		  line-height: 60px;
		  		  text-align: center;
		  		  height: 60px;
		  		}
		
		.rgs-form /deep/ .el-upload-list .el-upload-list__item {
		  		  border: 1px dashed  rgba(167, 180, 201,.3)    ;
		  		  cursor: pointer;
		  		  border-radius: 8px;
		  		  color: rgba(167, 180, 201,.3);
		  		  width: 90px;
		  		  font-size: 32px;
		  		  line-height: 60px;
		  		  text-align: center;
		  		  height: 60px;
		  		}
		
		.rgs-form /deep/ .el-upload .el-icon-plus {
		  		  border: 1px dashed  rgba(167, 180, 201,.3)    ;
		  		  cursor: pointer;
		  		  border-radius: 8px;
		  		  color: rgba(167, 180, 201,.3);
		  		  width: 90px;
		  		  font-size: 32px;
		  		  line-height: 60px;
		  		  text-align: center;
		  		  height: 60px;
		  		}
	}
	.required {
		position: relative;
	}
	.required::after{
				color: red;
				left: -10px;
				position: absolute;
				content: "*";
			}
	.editor>.avatar-uploader {
		line-height: 0;
		height: 0;
	}
</style>
