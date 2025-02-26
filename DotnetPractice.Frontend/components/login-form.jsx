/* eslint-disable prettier/prettier */
import React, { useRef, useState } from "react";
import { IoEye } from "react-icons/io5";
import { IoEyeOff } from "react-icons/io5";
import { useMutation } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { useRouter } from "next/router";
import { Card, CardHeader, CardBody } from "@heroui/react";
import { Divider } from "@heroui/react";
import { Input } from "@heroui/react";
import { Button } from "@heroui/react";

import { Login } from "@/requests/mutations/Login";
export default function LoginForm() {
  const router = useRouter();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isVisible, setIsVisible] = React.useState(false);
  const [loading, setLoading] = useState(false);
  const toggleVisibility = () => setIsVisible(!isVisible);
  const toastId = useRef(null);
  const mutation = useMutation({
    mutationFn: Login,
    onSuccess: (response) => {
      setLoading(false);
      //saving the token in local storage
      localStorage.setItem("tk", response.data.accessToken);
      localStorage.setItem("uid", response.data.id);
      localStorage.setItem("name", response.data.name);
      toast.update(toastId.current, {
        render: "Logged In Successfully",
        type: "success",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
        isLoading: false,
      });
      router.push("/panel/dashboard");
    },
    onError: (_error) => {
      setLoading(false);
      toast.update(toastId.current, {
        render: "Wrong Credentials",
        type: "error",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
        isLoading: false,
      });
    },
  });
  const doLogin = () => {
    setLoading(true);
    toastId.current = toast.loading("Please Wait ...", {
      position: "bottom-center",
    });
    mutation.mutate({ username, password });
  };

  return (
    <>
      <div className="w-full grid grid-cols-12 gap-2">
        <div className="md:col-start-4 lg:col-start-4 xl:col-start-5 col-span-12 sm:col-span-12 md:col-span-6 lg:col-span-6 xl:col-span-4">
          <Card>
            <CardHeader className="justify-center primary_color">
              <h5>Login Form</h5>
            </CardHeader>
            <Divider />
            <CardBody>
              <div>
                <Input
                  color="primary"
                  placeholder="Enter Username Here"
                  type="username"
                  value={username}
                  onValueChange={(value) => setUsername(value)}
                />
                <Input
                  className="py-3"
                  color="primary"
                  endContent={
                    <button
                      className="focus:outline-none"
                      type="button"
                      onClick={toggleVisibility}
                    >
                      {isVisible ? (
                        <IoEye className="text-2xl text-default-400 pointer-events-none text-primary" />
                      ) : (
                        <IoEyeOff className="text-2xl text-default-400 pointer-events-none text-primary" />
                      )}
                    </button>
                  }
                  placeholder="Enter Password Here"
                  type={isVisible ? "text" : "password"}
                  value={password}
                  onValueChange={(value) => setPassword(value)}
                />
              </div>
              <div className="flex py-1 justify-center items-center">
                <Button
                  className="text-white"
                  color="primary"
                  isLoading={loading}
                  onPress={doLogin}
                >
                  Login
                </Button>
              </div>
            </CardBody>
          </Card>
        </div>
      </div>
    </>
  );
}
