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

import { Signup } from "@/requests/mutations/Signup";
export default function SignupForm() {
  const router = useRouter();
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [isVisible, setIsVisible] = React.useState(false);
  const [loading, setLoading] = useState(false);
  const toggleVisibility = () => setIsVisible(!isVisible);
  const toastId = useRef(null);
  const mutation = useMutation({
    mutationFn: Signup,
    onSuccess: (_response) => {
      setLoading(false);
      //saving the token in local storage
      toast.update(toastId.current, {
        render: "ُSigned Up Successfully!",
        type: "success",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
        isLoading: false,
      });
      setTimeout(() => {
        router.push("/login");
      }, 1000);
    },
    onError: (error) => {
      setLoading(false);
      const message = error?.response?.data?.message;
      toast.update(toastId.current, {
        render: message ? message : "Unknown Error",
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
  const doSignup = () => {
    // checking password here
    setLoading(true);
    toastId.current = toast.loading("Please Wait ...", {
      position: "bottom-center",
    });
    if (password != confirmPassword) {
      toast.update(toastId.current, {
        render: "ُPassword and confirm password must be equal",
        type: "error",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
        isLoading: false,
      });
      setLoading(false);
      return;
    }
    mutation.mutate({ username, password, name, email });
  };

  return (
    <>
      <div className="w-full grid grid-cols-12 gap-2">
        <div className="md:col-start-4 lg:col-start-4 xl:col-start-5 col-span-12 sm:col-span-12 md:col-span-6 lg:col-span-6 xl:col-span-4">
          <Card>
            <CardHeader className="justify-center primary_color">
              <h5>Signup Form</h5>
            </CardHeader>
            <Divider />
            <CardBody>
              <div>
                <Input
                  color="primary"
                  isRequired={true}
                  placeholder="Username"
                  required={true}
                  type="text"
                  value={username}
                  onValueChange={(value) => setUsername(value)}
                />
                <Input
                  className="pt-3"
                  color="primary"
                  placeholder="Name"
                  required={false}
                  type="text"
                  value={name}
                  onValueChange={(value) => setName(value)}
                />
                <Input
                  className="pt-3"
                  color="primary"
                  placeholder="Email"
                  required={false}
                  type="email"
                  value={email}
                  onValueChange={(value) => setEmail(value)}
                />
                <Input
                  className="pt-3"
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
                  placeholder="Password"
                  type={isVisible ? "text" : "password"}
                  value={password}
                  onValueChange={(value) => setPassword(value)}
                />
                <Input
                  className="pt-3"
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
                  placeholder="Confirm Password"
                  type={isVisible ? "text" : "password"}
                  value={confirmPassword}
                  onValueChange={(value) => setConfirmPassword(value)}
                />
              </div>
              <div className="flex py-3 justify-center items-center">
                <Button
                  className="text-white"
                  color="primary"
                  isLoading={loading}
                  onPress={doSignup}
                >
                  Sign Up
                </Button>
              </div>
            </CardBody>
          </Card>
        </div>
      </div>
    </>
  );
}
